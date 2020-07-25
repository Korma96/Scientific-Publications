using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Service.CoverLetter
{
    public interface ICoverLetterService : ISingletonService
    {
        Task<MemoryStream> GetXsdSchemaAsync();

        void ValidateCoverLetterFile(string fileContent);

        Task InsertAsync(string fileContent);

        Task<DataAccess.Model.CoverLetter> FindByPublicationIdAsync(string publicationId);
    }
}
