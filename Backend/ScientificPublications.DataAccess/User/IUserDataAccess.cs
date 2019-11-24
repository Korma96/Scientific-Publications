using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.User
{
    public interface IUserDataAccess : IDataAccess
    {
        Task<Model.User> FindByUsername(string username);

        Task Insert(Model.User newUser);
    }
}
