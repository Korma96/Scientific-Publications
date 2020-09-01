using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.Model;
using ScientificPublications.DataAccess.Review;
using ScientificPublications.Service.Publication;
using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Review
{
    public class ReviewService : AbstractService, IReviewService
    {
        private readonly IReviewDataAccess _reviewDataAccess;

        private readonly IPublicationService _publicationService;

        public ReviewService(
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            IReviewDataAccess reviewDataAccess,
            IPublicationService publicationService)
            : base(appSettings, mapper)
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

        public async Task AddEvalutionAsync(string publicationId, evaluation evaluation)
        {
            var status = await _publicationService.GetStatusAsync(publicationId);
            if (status != PublicationStatus.IN_REVIEW)
            {
                throw new ValidationException("Invalid current publication status");
            }

            ValidateEvaluaionFile(XmlUtility.Serialize(evaluation));
            await _reviewDataAccess.AddEvaluationAsync(publicationId, evaluation);
        }

        public Task<Evaluations> GetEvaluationAsync(string publicationId, string reviewerUsername)
        {
            return _reviewDataAccess.GetEvaluationAsync(publicationId, reviewerUsername);
        }

        public Task<Evaluations> GetEvaluationsAsync(string publicationId)
        {
            return _reviewDataAccess.GetEvaluationsAsync(publicationId);
        }

        private void ValidateEvaluaionFile(string fileContent)
        {
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.EvaluationXsd);
            var xDocument = XmlUtility.Parse(fileContent);
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }
    }
}
