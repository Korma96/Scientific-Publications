using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
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
using ScientificPublications.Service.WorkFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Publication
{
    public class PublicationService : AbstractService, IPublicationService
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IPublicationDataAccess _publicationDataAccess;
        private readonly IWorkFlowService _workFlowService;

        public PublicationService(
            IOptions<AppSettings> appSettings, 
            IMapper mapper,
            IEmailService emailService,
            IPublicationDataAccess publicationDataAccess,
            IUserService userService,
            IWorkFlowService workFlowService) 
            : base(appSettings, mapper)
        {
            _emailService = emailService;
            _publicationDataAccess = publicationDataAccess;
            _userService = userService;
            _workFlowService = workFlowService;
        }

        public async Task<PublicationStatus> GetStatusAsync(string id)
        {
            var publication = await GetByIdWithValidationAsync(id);
            return HelperMethods.StatusStringToEnum(publication.header.status);
        }

        public async Task<publication> GetByIdWithValidationAsync(string id)
        {
            var publication = await GetByIdAsync(id);
            if (publication == null)
            {
                throw new ValidationException(Constants.ExceptionMessages.DoesNotExist);
            }
            return publication;
        }

        public Task<publication> GetByIdAsync(string id)
        {
            return _publicationDataAccess.FindByIdAsync(id);
        }
        
        public void ValidatePublicationFile(string fileContent)
        {
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationXsd);
            var xDocument = XmlUtility.Parse(fileContent);
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }

        public async Task<MemoryStream> GetXsdSchemaAsync()
        {
            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.PublicationXsd);
            return await FileUtility.ReadAsStreamAsync(path);
        }

        public Task InsertAsync(string fileContent)
        {
            var publication = XmlUtility.Deserialize<publication>(fileContent);
            publication.id = Guid.NewGuid().ToString();
            publication.header.status = PublicationStatus.SUBMITED.ToString().ToLower();
            return _publicationDataAccess.InsertAsync(publication);
        }

        // TO DO: return list of publications 
        public Task<Publications> FindByAuthorAsync(string author)
        {
            return _publicationDataAccess.FindByAuthor(author);
        }

        public Task<Publications> FindByStatusAsync(string status)
        {
            if (!new List<string>(Enum.GetNames(typeof(PublicationStatus))).Contains(status.ToUpper()))
            {
                throw new ValidationException("Status does not exist");
            }

            return _publicationDataAccess.FindByStatusAsync(status.ToLower());
        }

        public Task UpdateStatusAsync(string publicationId, PublicationStatus status)
        {
            return _publicationDataAccess.UpdateStatusAsync(publicationId, status);
        }

        public async Task UpdateStatusWithValidationAndEmailNotificationAsync(string publicationId, string nextStatus, string userRole, string username)
        {
            var currentStatus = await GetStatusAsync(publicationId);
            HelperMethods.CheckIsNextStateValid(currentStatus, nextStatus, userRole);
            await SendStateChangedEmailAsync(publicationId, nextStatus, userRole, username);
            await UpdateStatusAsync(publicationId, HelperMethods.StatusStringToEnum(nextStatus));
        }

        public Task<Publications> FindBySearchQueryAsync(string searchQuery)
        {
            return _publicationDataAccess.FindBySearchQueryAsync(searchQuery);
        }

        public Task<Publications> FindMyBySearchQueryAsync(string username, string searchQuery)
        {
            return _publicationDataAccess.FindByUsernameAndSearchQueryAsync(username, searchQuery);
        }

        public Task<Publications> FindByReviewerAsync(string reviewerUsername)
        {
            return _publicationDataAccess.FindByReviewerAsync(reviewerUsername);
        }

        public async Task SendAuthorUploadPublicationMail(string authorUsername, string publicationContent)
        {
            var editor = await _userService.FindByUsernameAsync(AppSettings.EditorUsername);
            var author = await _userService.FindByUsernameAsync(authorUsername);
            var publication = XmlUtility.Deserialize<publication>(publicationContent);

            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.AuthorUploadedMail);
            var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
            emailData.To = editor.Email;
            emailData.Body = string.Format(emailData.Body, editor.Name, publication.title, author.Name);
            await _emailService.SendEmailAsync(emailData);
        }

        public async Task SendAuthorRevisedPublicationMail(string publicationContent)
        {
            var editor = await _userService.FindByUsernameAsync(AppSettings.EditorUsername);
            var publication = XmlUtility.Deserialize<publication>(publicationContent);

            var path = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.AuthorRevisedMail);
            var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
            emailData.To = editor.Email;
            emailData.Body = string.Format(emailData.Body, editor.Name, publication.title);
            await _emailService.SendEmailAsync(emailData);
        }

        private async Task SendStateChangedEmailAsync(string publicationId, string nextStatus, string userRole, string username)
        {
            var statusEnum = HelperMethods.StatusStringToEnum(nextStatus);
            var roleEnum = HelperMethods.RoleStringToEnum(userRole);
            
            var workflow = await _workFlowService.FindByPublicationIdAsync(publicationId);
            var publication = await _publicationDataAccess.FindByIdAsync(publicationId);
            var authors = new List<DataAccess.Model.User>();
            foreach (var author in publication.authors)
            {
                authors.Add(await _userService.FindByUsernameAsync(author.username));
            }
            var editor = await _userService.FindByUsernameAsync(workflow.editor);
            var reviewers = new List<DataAccess.Model.User>();
            foreach (var reviewer in workflow.reviewers)
            {
                if (reviewer.status == ReviewerStatus.ACCEPTED.ToString().ToLower())
                    reviewers.Add(await _userService.FindByUsernameAsync(reviewer.username));
            }

            string xmlFilePath = string.Empty;
            string[] to = null;
            string[] args = null;

            switch (roleEnum)
            {
                case Role.Editor:
                    switch (statusEnum)
                    {
                        case PublicationStatus.ACCEPTED:
                            xmlFilePath = AppSettings.Paths.EditorAcceptedMail;
                            to = authors.Select(a => a.Email).ToArray();
                            args = new string[] { string.Join(",", authors.Select(a => a.Name).ToArray()), publication.title };
                            break;
                        case PublicationStatus.DENIED:
                            xmlFilePath = AppSettings.Paths.EditorDeniedMail;
                            to = authors.Select(a => a.Email).ToArray();
                            args = new string[] { string.Join(",", authors.Select(a => a.Name).ToArray()), publication.title };
                            break;
                        case PublicationStatus.SHOULD_REVISE:
                            xmlFilePath = AppSettings.Paths.EditorShouldReviseMail;
                            to = authors.Select(a => a.Email).ToArray();
                            args = new string[] { string.Join(",", authors.Select(a => a.Name).ToArray()), publication.title };
                            break;
                        case PublicationStatus.IN_REVIEW:
                            xmlFilePath = AppSettings.Paths.EditorInReviewMail;
                            to = reviewers.Select(r => r.Email).ToArray();
                            args = new string[] { string.Join(",", reviewers.Select(r => r.Name).ToArray()), publication.title };
                            break;
                    }
                    break;
                case Role.Reviewer:
                    switch (statusEnum)
                    {
                        case PublicationStatus.REVIEWED:
                            xmlFilePath = AppSettings.Paths.ReviewerReviewed;
                            to = new string[] { editor.Email };
                            var currentReviewer = reviewers.FirstOrDefault(r => r.Username == username);
                            args = new string[] { editor.Name, publication.title,  currentReviewer.Name };
                            break;
                    }
                    break;
                case Role.Author:
                    switch (statusEnum)
                    {
                        case PublicationStatus.WITHDRAWN:
                            xmlFilePath = AppSettings.Paths.AuthorWithdrawnMail;
                            var toList = new List<string>();
                            toList.AddRange(reviewers.Select(r => r.Email).ToArray());
                            toList.Add(editor.Email);
                            to = toList.ToArray();
                            args = new string[] { editor.Name, publication.title };
                            break;
                        case PublicationStatus.REVISED:
                            xmlFilePath = AppSettings.Paths.AuthorRevisedMail;
                            var toList2 = new List<string>();
                            toList2.AddRange(reviewers.Select(r => r.Email).ToArray());
                            toList2.Add(editor.Email);
                            to = toList2.ToArray();
                            args = new string[] { editor.Name, publication.title };
                            break;
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(xmlFilePath))
            {
                var path = Path.Combine(AppSettings.Paths.BasePath, xmlFilePath);
                var emailData = XmlUtility.DeserializeFromFile<EmailEntity>(path);
                var body = string.Format(emailData.Body, args);
                await _emailService.SendEmailAsync(emailData.Subject, body, to);
            }
        }

        public async Task InsertRevisionAsync(string fileContent, string previousPublicationId)
        {
            // TODO: add relationship between publication versions
            var currentStatus = await GetStatusAsync(previousPublicationId);
            if (currentStatus != PublicationStatus.SHOULD_REVISE)
                throw new ValidationException("Invalid current publication status");

            var workflow = HelperMethods.ThrowIfNullOtherwiseReturn(
                await _workFlowService.FindByPublicationIdAsync(previousPublicationId)) as workflow;
            var revisionId = await InsertRevisionAsync(fileContent);

            await _workFlowService.DeleteByPublicationIdAsync(previousPublicationId);
            workflow.publicationId = revisionId;
            await _workFlowService.InsertAsync(workflow);
        }

        public async Task<MemoryStream> DownloadPublicationAsPdfAsync(string publicationId)
        {
            return await _publicationDataAccess.GetPublicationAsPdfAsync(publicationId);
        }

        public async Task<MemoryStream> DownloadPublicationAsHtmlAsync(string publicationId)
        {
            return await _publicationDataAccess.GetPublicationAsHtmlAsync(publicationId);
        }

        private async Task<string> InsertRevisionAsync(string fileContent)
        {
            var publication = XmlUtility.Deserialize<publication>(fileContent);
            publication.id = Guid.NewGuid().ToString();
            publication.header.status = PublicationStatus.REVISED.ToString().ToLower();
            await _publicationDataAccess.InsertAsync(publication);
            return publication.id;
        }

        public Task<Publications> GetReferencingPublications(string publicationId)
        {
            return _publicationDataAccess.GetReferencingPublicationsAsync(publicationId);
        }
    }
}
