using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using ScientificPublications.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.User
{
    public class UserDataAccess : AbstractDataAccess, IUserDataAccess
    {
        public UserDataAccess(IOptions<AppSettings> appSettings, ILogger<UserDataAccess> logger) 
            : base(appSettings, logger, Constants.JavaController.User) { }

        public async Task<Users> FindAllReviewersAsync()
        {
            try
            {
                var path = BaseUrl.UrlCombine("reviewers");
                return await HttpHelper.Get<Users>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

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
