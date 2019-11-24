using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using System;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.User
{
    public class UserDataAccess : AbstractDataAccess, IUserDataAccess
    {
        public UserDataAccess(IOptions<AppSettings> appSettings, ILogger<UserDataAccess> logger) 
            : base(appSettings, logger, "user") { }

        public async Task<Model.User> FindByUsername(string username)
        {
            try
            {
                var path = BaseUrl.UrlCombine(username);
                return await HttpHelper.Get<Model.User>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task Insert(Model.User user)
        {
            try
            {
                await HttpHelper.Post(BaseUrl, user);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
    }
}
