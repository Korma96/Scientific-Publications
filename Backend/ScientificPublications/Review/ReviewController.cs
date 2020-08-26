using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Settings;
using ScientificPublications.Service.Review;
using System.Threading.Tasks;

namespace ScientificPublications.Reviewer
{
    public class ReviewController : AbstractController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(
            IOptions<AppSettings> appSettings,
            IReviewService reviewService) 
            : base(appSettings)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Reviewer: add comment to publication (section)
        /// </summary>
        [HttpPost("{publicationId}/{sectionId}")]
        [AuthorizationFilter(Role.Reviewer)]
        public async Task<IActionResult> AddCommentAsync([FromRoute] string publicationId, [FromRoute] string sectionId, [FromBody] string comment)
        {
            await _reviewService.AddCommentAsync(publicationId, sectionId, comment);
            return Ok();
        }
    }
}
