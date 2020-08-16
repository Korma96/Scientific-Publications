using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.DataAccess.Publication;
using ScientificPublications.DataAccess.WorkFlow;
using ScientificPublications.Service.Email;
using ScientificPublications.Service.User;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificPublications.Service.WorkFlow
{
    public class WorkFlowService : AbstractService, IWorkFlowService
    {
        private readonly IWorkFlowDataAccess _workFlowDataAccess;
        private readonly IPublicationDataAccess _publicationDataAccess;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public WorkFlowService(
            IOptions<AppSettings> appSettings, 
            IMapper mapper,
            IWorkFlowDataAccess workFlowDataAccess,
            IPublicationDataAccess publicationDataAccess,
            IEmailService emailService,
            IUserService userService) 
            : base(appSettings, mapper)
        {
            _workFlowDataAccess = workFlowDataAccess;
            _publicationDataAccess = publicationDataAccess;
            _emailService = emailService;
            _userService = userService;
        }

        public Task InsertAsync(workflow workFlow)
        {
            return _workFlowDataAccess.InsertAsync(workFlow);
        }

        public void Validate(workflow workflow)
        {
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.WorkFlowXsd);
            var xDocument = XmlUtility.Parse(XmlUtility.Serialize(workflow));
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }

        public Task<workflow> FindByPublicationIdAsync(string publicationId)
        {
            return _workFlowDataAccess.FindByPublicationIdAsync(publicationId);
        }

        public async Task AcceptPublicationAsync(string publicationId, bool accepted, string reviewerUsername)
        {
            var publication = await _publicationDataAccess.FindByIdAsync(publicationId);
            if (publication == null)
                throw new ValidationException("Publication does not exist");

            var publicationStatus = HelperMethods.StatusStringToEnum(publication.header.status);
            if (!(publicationStatus == PublicationStatus.ASSIGNED || publicationStatus == PublicationStatus.IN_REVIEW))
                throw new ValidationException("Publication is in invalid status for this action");

            var workflow = await _workFlowDataAccess.FindByPublicationIdAsync(publicationId);
            if (workflow == null)
                throw new ValidationException("Workflow does not exist for this publication");

            var reviewerWorkflow = workflow.reviewers.FirstOrDefault(r => r.username == reviewerUsername);
            if (reviewerWorkflow == null)
                throw new ValidationException("Reviewer is not assigned for this publication");

            if (!reviewerWorkflow.status.Equals(ReviewerStatus.PENDING.ToString().ToLower()))
                throw new ValidationException("Reviewer already accepted or declined this publication");

            var reviewerStatus = accepted ? ReviewerStatus.ACCEPTED : ReviewerStatus.DECLINED;
            reviewerWorkflow.status = reviewerStatus.ToString().ToLower();

            var editor = await _userService.FindByUsernameAsync(workflow.editor);
            var reviewer = await _userService.FindByUsernameAsync(reviewerWorkflow.username);

            var shouldUpdateWorkFlow = true;
            if (accepted)
            {
                await SendReviewerAcceptPublicationEmailAsync(editor.Email, editor.Name, publication.title, reviewer.Name);
                if (publicationStatus == PublicationStatus.ASSIGNED)
                    await _publicationDataAccess.UpdateStatusAsync(publicationId, PublicationStatus.IN_REVIEW);
            }
            else
            {
                await SendReviewerDenyPublicationEmailAsync(editor.Email, editor.Name, publication.title, reviewer.Name);
                var statuses = workflow.reviewers.Select(r => r.status).ToList();
                if (!(statuses.Contains(ReviewerStatus.PENDING.ToString().ToLower()) ||
                      statuses.Contains(ReviewerStatus.ACCEPTED.ToString().ToLower())))
                {
                    await _publicationDataAccess.UpdateStatusAsync(publicationId, PublicationStatus.SUBMITED);
                    shouldUpdateWorkFlow = false;
                    await SendReviewersDenyPublicationEmailAsync(editor.Email, editor.Name, publication.title);
                }
            }
            
            await _workFlowDataAccess.DeleteAsync(publicationId);

            if (shouldUpdateWorkFlow)
            {
                await _workFlowDataAccess.InsertAsync(workflow);
            }
        }

        public async Task<List<ReviewerPublicationDto>> GetByReviewerAsync(string username)
        {
            var workflows = await _workFlowDataAccess.FindByReviewerAsync(username);
            return workflows.WorkFlowList.Select(w => new ReviewerPublicationDto
            {
                PublicationId = w.publicationId,
                ReviewerStatus = w.reviewers.FirstOrDefault(r => r.username == username).status
            }).ToList();
        }

        private async Task SendReviewerDenyPublicationEmailAsync(string editorEmail, string editorName, string publicationTitle, string reviewerName)
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.ReviewerDenied);
            var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
            emailData.To = editorEmail;
            emailData.Body = string.Format(emailData.Body, editorName, publicationTitle, reviewerName);
            await _emailService.SendEmailAsync(emailData);
        }

        private async Task SendReviewersDenyPublicationEmailAsync(string editorEmail, string editorName, string publicationTitle)
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.ReviewersDenied);
            var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
            emailData.To = editorEmail;
            emailData.Body = string.Format(emailData.Body, editorName, publicationTitle);
            await _emailService.SendEmailAsync(emailData);
        }

        private async Task SendReviewerAcceptPublicationEmailAsync(string editorEmail, string editorName, string publicationTitle, string reviewerName)
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.ReviewerAccepted);
            var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
            emailData.To = editorEmail;
            emailData.Body = string.Format(emailData.Body, editorName, publicationTitle, reviewerName);
            await _emailService.SendEmailAsync(emailData);
        }
    }
}
