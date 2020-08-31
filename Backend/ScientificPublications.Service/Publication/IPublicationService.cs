using ScientificPublications.Common.Enums;
using ScientificPublications.DataAccess.Model;
using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Publication
{
    public interface IPublicationService : ISingletonService
    {
        void ValidatePublicationFile(string fileContent);

        Task<MemoryStream> GetXsdSchemaAsync();

        Task InsertAsync(string fileContent);

        Task<Publications> FindByAuthorAsync(string author);

        Task<Publications> FindByStatusAsync(string status);

        Task UpdateStatusAsync(string publicationId, PublicationStatus status);

        Task<publication> GetByIdWithValidationAsync(string id);

        Task<publication> GetByIdAsync(string id);

        Task<PublicationStatus> GetStatusAsync(string id);

        Task UpdateStatusWithValidationAndEmailNotificationAsync(string publicationId, string nextStatus, string userRole, string username);

        Task<Publications> FindBySearchQueryAsync(string searchQuery);

        Task<Publications> FindMyBySearchQueryAsync(string username, string searchQuery);

        Task<Publications> FindByReviewerAsync(string reviewerUsername);

        Task SendAuthorUploadPublicationMail(string authorUsername, string fileContent);

        Task SendAuthorRevisedPublicationMail(string publicationContent);

        Task InsertRevisionAsync(string fileContent, string previousPublicationId);

        Task<MemoryStream> DownloadPublicationAsPdfAsync(string publicationId);

        Task<MemoryStream> DownloadPublicationAsHtmlAsync(string publicationId);

        Task<Publications> GetReferencingPublications(string publicationId);
    }
}
