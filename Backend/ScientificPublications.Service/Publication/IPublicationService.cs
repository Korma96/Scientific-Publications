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
    }
}
