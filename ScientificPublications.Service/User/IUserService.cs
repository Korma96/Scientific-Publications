using ScientificPublications.Common.Models;

namespace ScientificPublications.Service.User
{
    public interface IUserService : ISingletonService
    {
        UserDto Login(string username, string password);
    }
}
