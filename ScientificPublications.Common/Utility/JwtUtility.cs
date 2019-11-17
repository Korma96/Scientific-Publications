using Microsoft.IdentityModel.Tokens;
using ScientificPublications.Common.Models;
using ScientificPublications.Common.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScientificPublications.Common.Utility
{
    public static class JwtUtility
    {
        public static string CreateJwtToken(AppSettings appSettings, SessionDto sessionInfo)
        {
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Constants.SessionInfo, XmlUtility.Serialize(sessionInfo))
                }),
                Expires = DateTime.UtcNow.AddSeconds(appSettings.JwtTokenExpiresInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = appSettings.Issuer,
                Audience = appSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
