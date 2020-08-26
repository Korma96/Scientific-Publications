using System.Threading.Tasks;

namespace ScientificPublications.Service.Review
{
    public interface IReviewService : ISingletonService
    {
        Task AddCommentAsync(string publicationId, string sectionId, string comment);
    }
}
