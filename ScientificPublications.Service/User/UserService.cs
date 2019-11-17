using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Models;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.User;

namespace ScientificPublications.Service.User
{
    public class UserService : AbstractService, IUserService
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserService(
            IOptions<AppSettings> appSettings, 
            IMapper mapper, 
            IUserDataAccess userDataAccess) 
            : base(appSettings, mapper)
        {
            _userDataAccess = userDataAccess;
        }

        public UserDto Login(string username, string password)
        {
            var user = _userDataAccess.FindByUsername(username);
            if (user == null)
                throw new ValidationException(Constants.ExceptionMessages.InvalidUsernameOrPassword);

            if (!HashUtility.HashPassword(user.Salt, password).Equals(user.Password))
                throw new ValidationException(Constants.ExceptionMessages.InvalidUsernameOrPassword);

            return Mapper.Map<UserDto>(user);
        }
    }
}
