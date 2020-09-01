using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Settings;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.Review;
using ScientificPublications.Service.Review;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificPublications.Reviewer
{
    public class ReviewController : AbstractController
    {
        private readonly IReviewService _reviewService;

        private readonly IMapper _mapper;

        public ReviewController(
            IOptions<AppSettings> appSettings,
            IReviewService reviewService,
            IMapper mapper)
            : base(appSettings)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        /// <summary>
        /// Reviewer: add comment to publication (section)
        /// </summary>
        [HttpPost("comment/{publicationId}/{sectionId}")]
        [AuthorizationFilter(Role.Reviewer)]
        public async Task<IActionResult> AddCommentAsync([FromRoute] string publicationId, [FromRoute] string sectionId, [FromBody] string comment)
        {
            await _reviewService.AddCommentAsync(publicationId, sectionId, comment);
            return Ok();
        }

        /// <summary>
        /// Reviewer: add evaluation form for publication
        /// </summary>
        [HttpPost("evaluation/{publicationId}")]
        [AuthorizationFilter(Role.Reviewer)]
        public async Task<IActionResult> AddEvaluationAsync([FromRoute] string publicationId, [FromBody] EvaluationDto evaluationDto)
        {
            var evaluation = _mapper.Map<evaluation>(evaluationDto);
            evaluation.author = GetSession().Username;
            evaluation.publicationId = publicationId;
            await _reviewService.AddEvalutionAsync(publicationId, evaluation);
            return Ok();
        }

        /// <summary>
        /// Editor: Get evaluation by publication and reviewer
        /// </summary>
        [HttpGet("evaluation/{publicationId}/{reviewerUsername}")]
        [AuthorizationFilter(Role.Editor)]
        public async Task<IActionResult> GetEvaluationAsync([FromRoute] string publicationId, [FromRoute] string reviewerUsername)
        {
            var evaluation = await _reviewService.GetEvaluationAsync(publicationId, reviewerUsername);
            var evaluationDto = _mapper.Map<List<EvaluationDto>>(evaluation.EvaluationList);
            return Ok(evaluationDto);
        }

        /// <summary>
        /// Author: get evaluations for publication
        /// </summary>
        [HttpGet("evaluation/{publicationId}")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> GetEvaluationsAsync([FromRoute] string publicationId)
        {
            var evaluations = await _reviewService.GetEvaluationsAsync(publicationId);
            var evaluationDtos = _mapper.Map<List<EvaluationDto>>(evaluations.EvaluationList);
            return Ok(evaluationDtos);
        }
    }
}
