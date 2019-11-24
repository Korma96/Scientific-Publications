using ScientificPublications.Common.Models;
using System.Threading.Tasks;

namespace ScientificPublications.Service.User
{
    public interface IUserService : ISingletonService
    {
        Task<UserDto> Login(string username, string password);

        Task Register(RegisterDto registerDto);

        void Validate(RegisterDto registerDto);
    }
}
