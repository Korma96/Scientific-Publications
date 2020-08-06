using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.WorkFlow
{
    public interface IWorkFlowDataAccess : IDataAccess
    {
        Task InsertAsync(Model.workflow workFlow);
    }
}
