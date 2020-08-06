using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Settings;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.Service.Publication;
using ScientificPublications.Service.WorkFlow;
using System.Threading.Tasks;

namespace ScientificPublications.WorkFlow
{
    public class WorkFlowController : AbstractController
    {
        private readonly IWorkFlowService _workFlowService;

        private readonly IPublicationService _publicationService;

        public WorkFlowController(
            IOptions<AppSettings> appSettings,
            IWorkFlowService workFlowService,
            IPublicationService publicationService) 
            : base(appSettings)
        {
            _workFlowService = workFlowService;
            _publicationService = publicationService;
        }

        [HttpPost]
        [AuthorizationFilter(Role.Editor)]
        [Consumes(Constants.XmlContentType)]
        public async Task<IActionResult> Insert([FromBody] workflow workFlow)
        {
            _workFlowService.Validate(workFlow);
            // TODO: add validation if reviewers exist, and if publication with given id exist
            await _publicationService.UpdateStatusAsync(workFlow.publicationId, PublicationStatus.ASSIGNED);
            await _workFlowService.InsertAsync(workFlow);
            return Ok();
        }
    }
}
