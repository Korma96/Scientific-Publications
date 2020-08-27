using ScientificPublications.Common.Enums;
using ScientificPublications.DataAccess.Model;
using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.Publication
{
    public interface IPublicationDataAccess : IDataAccess
    {
        Task<Publications> FindByAuthor(string authorUsername);

        Task InsertAsync(publication publication);

        Task<Publications> FindByStatusAsync(string status);

        Task UpdateStatusAsync(string publicationId, PublicationStatus status);

        Task<publication> FindByIdAsync(string id);

        Task<Publications> FindBySearchQueryAsync(string searchQuery);

        Task<Publications> FindByUsernameAndSearchQueryAsync(string username, string searchQuery);

        Task<Publications> FindByReviewerAsync(string reviewerUsername);

        Task<MemoryStream> GetPublicationAsPdfAsync(string publicationId);

        Task<MemoryStream> GetPublicationAsHtmlAsync(string publicationId);
    }
}
