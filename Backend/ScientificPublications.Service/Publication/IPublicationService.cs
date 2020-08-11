using ScientificPublications.Common.Enums;
using ScientificPublications.DataAccess.Model;
using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Publication
{
    public interface IPublicationService : ISingletonService
    {
        Task AcceptPublicationAsync(string email, string authorName, string publicationTitle);

        Task DenyPublicationAsync(string email, string authorName, string publicationTitle, string text);

        void ValidatePublicationFile(string fileContent);

        Task<MemoryStream> GetXsdSchemaAsync();

        Task InsertAsync(string fileContent);

        Task<Publications> FindByAuthorAsync(string author);

        Task<Publications> FindByStatusAsync(string status);

        Task UpdateStatusAsync(string publicationId, PublicationStatus status);

        Task<publication> GetByIdWithValidationAsync(string id);

        Task<publication> GetByIdAsync(string id);

        Task<PublicationStatus> GetStatusAsync(string id);

        Task UpdateStatusWithValidationAsync(string publicationId, string nextStatus, string userRole);

        Task<Publications> FindBySearchQueryAsync(string searchQuery);

        Task<Publications> FindMyBySearchQueryAsync(string username, string searchQuery);
    }
}
