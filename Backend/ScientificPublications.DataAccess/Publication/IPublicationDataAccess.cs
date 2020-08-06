using ScientificPublications.Common.Enums;
using ScientificPublications.DataAccess.Model;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.Publication
{
    public interface IPublicationDataAccess : IDataAccess
    {
        Task<Publications> FindByAuthor(string authorUsername);

        Task InsertAsync(Model.publication publication);

        Task<Publications> FindByStatusAsync(string status);

        Task UpdateStatusAsync(string publicationId, PublicationStatus status);

        Task<publication> FindByIdAsync(string id);
    }
}
