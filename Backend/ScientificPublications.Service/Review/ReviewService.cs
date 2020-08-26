using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.DataAccess.Review;
using ScientificPublications.Service.Publication;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Review
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewDataAccess _reviewDataAccess;

        private readonly IPublicationService _publicationService;

        public ReviewService(
            IReviewDataAccess reviewDataAccess,
            IPublicationService publicationService)
        {
            _reviewDataAccess = reviewDataAccess;
            _publicationService = publicationService;
        }

        public async Task AddCommentAsync(string publicationId, string sectionId, string comment)
        {
            var status = await _publicationService.GetStatusAsync(publicationId);
            if (status != PublicationStatus.IN_REVIEW)
            {
                throw new ValidationException("Invalid current publication status");
            }

            await _reviewDataAccess.AddCommentAsync(publicationId, sectionId, comment);
        }
    }
}
