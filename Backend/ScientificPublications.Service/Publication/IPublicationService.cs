using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.Publication
{
    public interface IPublicationService : ISingletonService
    {
        Task AcceptPublicationAsync(string email);

        Task DenyPublicationAsync(string email, string text);

        void ValidatePublicationFile(string fileContent);

        Task<MemoryStream> GetXsdSchemaAsync();
    }
}
