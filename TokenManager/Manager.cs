using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TokenManager.Models;

namespace TokenManager
{
    public static class Manager
    {
        private static readonly double _expireTimeAsMinutes = Convert.ToDouble(Environment.GetEnvironmentVariable("JwtExpireDurationMinutes"));
        private static readonly string _jwtSecretKey = Environment.GetEnvironmentVariable("JwtSecretKey");
        private static readonly byte[] _encodedJwtKey = Encoding.ASCII.GetBytes(_jwtSecretKey);

        public static string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expireTimeAsMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_encodedJwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
