using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.Review
{
    public interface IReviewDataAccess : IDataAccess
    {
        Task AddCommentAsync(string publicationId, string sectionId, string comment);
    }
}
