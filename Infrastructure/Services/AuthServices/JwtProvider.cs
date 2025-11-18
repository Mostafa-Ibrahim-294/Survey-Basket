using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Options;
using Application.Contracts.Authentication;
using Domain.Entites;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Infrastructure.Services.AuthServices
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public (string token, DateTime expiresOn) GenerateRefreshToken()
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var expiresOn = DateTime.UtcNow.AddDays(14);
            return (refreshToken, expiresOn);
        }

        public (string token, int expiresIn) GenerateToken(ApplicationUser applicationUser , IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.GivenName , applicationUser.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName , applicationUser.LastName),
                new Claim(JwtRegisteredClaimNames.Email , applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var expiresIn = _jwtOptions.Value.ExpiryMinutes;
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (token, expiresIn * 60);
        }
    }
}
