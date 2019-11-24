using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ScientificPublications.Common.Settings;
using System.Text;

namespace ScientificPublications.Application
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddJwtBearerAuth(this AuthenticationBuilder builder, IConfigurationSection appSettingsSection)
        {
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Jwt.Secret);

            return builder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = appSettings.Jwt.Audience,
                    ValidIssuer = appSettings.Jwt.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }
    }
}
