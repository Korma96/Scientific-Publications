using ScientificPublications.DataAccess.Model;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.WorkFlow
{
    public interface IWorkFlowDataAccess : IDataAccess
    {
        Task InsertAsync(workflow workFlow);

        Task<workflow> FindByPublicationIdAsync(string publicationId);
    }
}
