using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.Service.Publication;
using ScientificPublications.Service.User;
using ScientificPublications.Service.WorkFlow;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificPublications.WorkFlow
{
    public class WorkFlowController : AbstractController
    {
        private readonly IWorkFlowService _workFlowService;

        private readonly IPublicationService _publicationService;

        private readonly IUserService _userService;

        public WorkFlowController(
            IOptions<AppSettings> appSettings,
            IWorkFlowService workFlowService,
            IPublicationService publicationService,
            IUserService userService) 
            : base(appSettings)
        {
            _workFlowService = workFlowService;
            _publicationService = publicationService;
            _userService = userService;
        }

        [HttpPost]
        [AuthorizationFilter(Role.Editor)]
        [Consumes(Constants.XmlContentType)]
        public async Task<IActionResult> Insert([FromBody] workflow workFlow)
        {
            _workFlowService.Validate(workFlow);
            var tasks = new List<Task>();
            foreach (var reviewerUsername in workFlow.reviewers)
            {
                tasks.Add(_userService.FindByUsernameAsync(reviewerUsername));
            }
            tasks.Add(_userService.FindByUsernameAsync(workFlow.editor));
            await Task.WhenAll(tasks.ToArray());

            var status = await _publicationService.GetStatusAsync(workFlow.publicationId);
            if (status != PublicationStatus.SUBMITED)
            {
                throw new ValidationException("Invalid status");
            }

            HelperMethods.ThrowIfNotNull(await _workFlowService.FindByPublicationIdAsync(workFlow.publicationId));

            await _publicationService.UpdateStatusAsync(workFlow.publicationId, PublicationStatus.ASSIGNED);
            await _workFlowService.InsertAsync(workFlow);
            return Ok();
        }
    }
}
