using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Settings;
using System;

namespace ScientificPublications.DataAccess
{
    public class AbstractDataAccess
    {
        protected readonly AppSettings AppSettings;
        protected readonly ILogger<AbstractDataAccess> Logger;
        protected string BaseUrl { get; private set; }

        protected AbstractDataAccess(IOptions<AppSettings> appSettings, ILogger<AbstractDataAccess> logger, string controllerName)
        {
            AppSettings = appSettings.Value;
            Logger = logger;
            BaseUrl = AppSettings.DbProxyUrl.UrlCombine(controllerName);
        }

        // to do: investigate how to run every data access method with this func
        protected T RunSafely<S, T>(S param, Func<S, T> func)
        {
            try
            {
                return func.Invoke(param);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        protected void RunSafely<S>(S param, Action<S> action)
        {
            try
            {
                action.Invoke(param);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
    }
}
