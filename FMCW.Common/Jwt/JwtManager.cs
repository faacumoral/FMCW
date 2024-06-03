using FMCW.Common.Results;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FMCW.Common.Jwt
{
    public class JwtManager : IJwtManager
    {
        private readonly JwtConfiguration _jwtConfiguration;

        private const string USER_ID_CLAIM = "UserId";
        public JwtManager(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public JwtDTO GenerateToken(string userId)
        {
            string issuer = _jwtConfiguration.Issuer;
            string audience = _jwtConfiguration.Audience;
            string secretKey = _jwtConfiguration.SecretKey;
            var exp = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpireMinutes);

            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(USER_ID_CLAIM, userId)
            };

            var _Payload = new JwtPayload(
                    issuer: issuer,
                    audience: audience,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: exp
                );

            var _Token = new JwtSecurityToken(_Header, _Payload);

            return new JwtDTO
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(_Token),
                ExpDate = exp
            };
        }

        public IntResult ValidateToken(string token)
        {
            try
            {
                string issuer = _jwtConfiguration.Issuer;
                string audience = _jwtConfiguration.Audience;

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));

                var tokenValidationParamters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateActor = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = secretKey
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParamters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return IntResult.Error("Invalid token");

                var userId = int.Parse(principal.FindFirst(USER_ID_CLAIM)?.Value ?? "-1");

                if (userId == -1)
                    return IntResult.Error("Missing claim");

                if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
                    return IntResult.Error("Expired token");
                else
                    return IntResult.Ok(userId);

            }
            catch (Exception ex)
            {
                return IntResult.Error(ex);
            }
        }

    }
}
