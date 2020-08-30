using System.Threading.Tasks;
using ScientificPublications.DataAccess.Model;

namespace ScientificPublications.Service.Review
{
    public interface IReviewService : ISingletonService
    {
        Task AddCommentAsync(string publicationId, string sectionId, string comment);

        Task AddEvalutionAsync(string publicationId, evaluation evaluation);

        Task<Evaluations> GetEvaluationAsync(string publicationId, string reviewerUsername);

        Task<Evaluations> GetEvaluationsAsync(string publicationId);
    }
}
