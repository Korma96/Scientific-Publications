using System.Threading.Tasks;
using ScientificPublications.DataAccess.Model;

namespace ScientificPublications.Service.WorkFlow
{
    public interface IWorkFlowService : ISingletonService
    {
        void Validate(workflow workflow);

        Task InsertAsync(workflow workFlow);

        Task<workflow> FindByPublicationIdAsync(string publicationId);
    }
}
