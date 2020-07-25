using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.CoverLetter
{
    public interface ICoverLetterDataAccess : IDataAccess
    {
        Task InsertAsync(Model.CoverLetter coverLetter);

        Task<Model.CoverLetter> FindByPublicationIdAsync(string publicationId);
    }
}
