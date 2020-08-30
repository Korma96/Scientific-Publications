using System.Threading.Tasks;
using ScientificPublications.DataAccess.Model;

namespace ScientificPublications.DataAccess.Review
{
    public interface IReviewDataAccess : IDataAccess
    {
        Task AddCommentAsync(string publicationId, string sectionId, string comment);

        Task AddEvaluationAsync(string publicationId, evaluation evaluation);

        Task<Evaluations> GetEvaluationsAsync(string publicationId);

        Task<Evaluations> GetEvaluationAsync(string publicationId, string reviewerUsername);
    }
}
