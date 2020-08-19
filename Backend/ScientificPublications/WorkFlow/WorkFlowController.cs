using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.Service.Publication;
using ScientificPublications.Service.User;
using ScientificPublications.Service.WorkFlow;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificPublications.WorkFlow
{
    public class WorkFlowController : AbstractController
    {
        private readonly IWorkFlowService _workFlowService;

        private readonly IPublicationService _publicationService;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public WorkFlowController(
            IOptions<AppSettings> appSettings,
            IWorkFlowService workFlowService,
            IPublicationService publicationService,
            IUserService userService,
            IMapper mapper)
            : base(appSettings)
        {
            _workFlowService = workFlowService;
            _publicationService = publicationService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [AuthorizationFilter(Role.Editor)]
        public async Task<IActionResult> Insert([FromBody] WorkFlowDto workFlowDto)
        {
            var workFlow = _mapper.Map<workflow>(workFlowDto);
            workFlow.editor = GetSession().Username;
            _workFlowService.Validate(workFlow);
            var tasks = new List<Task>();
            foreach (var reviewer in workFlow.reviewers)
            {
                tasks.Add(_userService.FindByUsernameAsync(reviewer.username));
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
            await _workFlowService.SendEmailToReviewersAsync(workFlow);
            return Ok();
        }

        [HttpGet("reviewer")]
        [AuthorizationFilter(Role.Reviewer)]
        public async Task<IActionResult> FindByReviewerAsync()
        {
            var workflows = await _workFlowService.GetByReviewerAsync(GetSession().Username);
            return Ok(workflows);
        }

        [HttpPut("{publicationId}/{accepted}")]
        [AuthorizationFilter(Role.Reviewer)]
        public async Task<IActionResult> AcceptPublicationAsync([FromRoute] string publicationId, [FromRoute] bool accepted)
        {
            await _workFlowService.AcceptPublicationAsync(publicationId, accepted, GetSession().Username);
            return Ok();
        }
    }
}
