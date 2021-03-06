using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Bloggy.API.Infrastructure;
using Bloggy.API.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Bloggy.API.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtTokenGenerator (IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        private static long ToUnixEpochDate (DateTime date) => (long) Math.Round ((date.ToUniversalTime () - new DateTimeOffset (1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public async Task<string> CreateToken (string username)
        {
            var claims = new []
            {
                new Claim (JwtRegisteredClaimNames.Sub, username),
                new Claim (JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator ()),
                new Claim (JwtRegisteredClaimNames.Iat,
                ToUnixEpochDate (_jwtOptions.IssuedAt).ToString (),
                ClaimValueTypes.Integer64)
            };
            var jwt = new JwtSecurityToken (
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler ().WriteToken (jwt);
            return encodedJwt;
        }
    }
}