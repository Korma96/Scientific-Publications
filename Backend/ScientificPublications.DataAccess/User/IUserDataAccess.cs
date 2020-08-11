using ScientificPublications.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.User
{
    public interface IUserDataAccess : IDataAccess
    {
        Task<Model.User> FindByUsername(string username);

        Task Insert(Model.User newUser);

        Task<Users> FindAllReviewersAsync();
    }
}
