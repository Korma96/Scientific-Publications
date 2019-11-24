using AutoMapper;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Models;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.User;
using System.IO;
using System.Threading.Tasks;

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

        public async Task<UserDto> Login(string username, string password)
        {
            var user = await _userDataAccess.FindByUsername(username);
            if (user == null)
                throw new ValidationException(Constants.ExceptionMessages.InvalidUsernameOrPassword);

            if (!HashUtility.HashPassword(user.Salt, password).Equals(user.Password))
                throw new ValidationException(Constants.ExceptionMessages.InvalidUsernameOrPassword);

            return Mapper.Map<UserDto>(user);
        }

        public async Task Register(RegisterDto registerDto)
        {
            var existingUser = await _userDataAccess.FindByUsername(registerDto.Username);
            if (existingUser != null)
                throw new ValidationException(Constants.ExceptionMessages.UsernameAlreadyExists);

            var salt = HashUtility.GetNewSalt();
            var hashedPassword = HashUtility.HashPassword(salt, registerDto.Password);
            var newUser = Mapper.Map<DataAccess.Model.User>(registerDto);
            newUser.Salt = salt;
            newUser.Password = hashedPassword;

            await _userDataAccess.Insert(newUser);
        }

        public void Validate(RegisterDto registerDto)
        {
            var xsdSchemaPath = Path.Combine(AppSettings.Paths.BasePath, AppSettings.Paths.UserDtoXsd);
            var xmlContent = XmlUtility.Serialize(registerDto);
            var xDocument = XmlUtility.Parse(xmlContent);
            XmlUtility.ValidateXmlAgainstXsd(xDocument, xsdSchemaPath);
        }
    }
}
