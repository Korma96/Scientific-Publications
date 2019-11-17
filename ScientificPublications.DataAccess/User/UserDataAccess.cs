using Microsoft.Extensions.Options;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.DataAccess.Model;
using System.IO;
using System.Linq;

namespace ScientificPublications.DataAccess.User
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly AppSettings _appSettings;

        private string UsersPath { get => Path.Combine(_appSettings.Paths.BasePath, _appSettings.Paths.Users); }

        public UserDataAccess(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Model.User FindByUsername(string username)
        {
            var allUsers = XmlUtility.DeserializeFromFile<AllUsers>(UsersPath);
            return allUsers.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
