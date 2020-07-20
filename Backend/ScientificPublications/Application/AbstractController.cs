using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Models;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using System.IdentityModel.Tokens.Jwt;

namespace ScientificPublications.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AbstractController : ControllerBase
    {
        protected AppSettings AppSettings { get; private set; }

        public AbstractController(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

        internal SessionDto GetSession()
        {
            var accessToken = ReadAccessToken();
            if (string.IsNullOrEmpty(accessToken))
                return null;

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            if (!jwt.Issuer.Equals(AppSettings.Jwt.Issuer))
                return null;

            try
            {
                return XmlUtility.Deserialize<SessionDto>(jwt.Payload[Constants.SessionInfo].ToString());
            }
            catch
            {
                return null;
            }
        }

        private string ReadAccessToken()
        {
            var authHeader = Request.Headers[Constants.Authorization].ToString();
            if (string.IsNullOrEmpty(authHeader))
                return null;

            var tokenPrefixLength = $"{JwtBearerDefaults.AuthenticationScheme}{Constants.SingleSpace}".Length;
            if (authHeader.Length < tokenPrefixLength)
                return null;

            var idToken = authHeader.Substring(tokenPrefixLength).Trim();
            return idToken;
        }
    }
}
