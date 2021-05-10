using Microsoft.IdentityModel.Tokens;
using Register.Business.Models;
using Register.DataAccess.Entities;
using Register.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Register.Business.Hub.Implementation
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IAuthenticationRepo _authenticationRepo;
        private readonly byte[] _signingKey;

        public AuthenticationManager(IAuthenticationRepo authenticationRepo)
        {
            _authenticationRepo = authenticationRepo;
            _signingKey = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecretKey"));
        }

        private TokenDTOResponse GenerateToken(int accessTokenExpiration, int refreshTokenExpiration, params (string, string)[] claims)
        {
            var now = DateTime.UtcNow;

            var accessTokenExpiresIn = (int)TimeSpan.FromMinutes(accessTokenExpiration).TotalSeconds;
            var refreshTokenExpiresIn = (int)TimeSpan.FromMinutes(refreshTokenExpiration).TotalSeconds;

            var userClaims = claims.Select(claim => new Claim(claim.Item1, claim.Item2)).ToArray();

            var accessToken = new JwtSecurityToken(
                claims: userClaims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(accessTokenExpiration)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_signingKey), SecurityAlgorithms.HmacSha256)
            );

            var handler = new JwtSecurityTokenHandler();

            var encodedAccessToken = handler.WriteToken(accessToken);

            var refreshToken = Guid.NewGuid().ToString();

            var authentication = new Authentication()
            {
                AccessToken = encodedAccessToken,
                RefreshToken = refreshToken,
                RefreshTokenSeconds = refreshTokenExpiresIn
            };

            _authenticationRepo.Insert(authentication);
            _authenticationRepo.SaveChanges();

            var responseJson = new TokenDTOResponse
            {
                AccessToken = encodedAccessToken,
                AccessTokenExpiresIn = (int)TimeSpan.FromMinutes(accessTokenExpiration).TotalSeconds,
                RefreshToken = refreshToken,
                RefreshTokenExpiresIn = (int)TimeSpan.FromMinutes(refreshTokenExpiration).TotalSeconds
            };

            return responseJson;
        }

        private TokenDTOResponse RefreshToken(int accessTokenExpiration, int refreshTokenExpiration, string refreshToken)
        {
            var authentication = _authenticationRepo.First(refreshToken);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authentication.AccessToken);
            var userClaims = token.Claims.Where(x => x.Type != "exp" && x.Type != "nbf").Select(x => (x.Type, x.Value)).ToArray();

            var tokenResponse = GenerateToken(accessTokenExpiration, refreshTokenExpiration, userClaims);

            return tokenResponse;
        }


        TokenDTOResponse IAuthenticationManager.GenerateToken(int accessTokenExpiration, int refreshTokenExpiration, params (string, string)[] claims)
            => GenerateToken(accessTokenExpiration, refreshTokenExpiration, claims);

        TokenDTOResponse IAuthenticationManager.RefreshToken(int accessTokenExpiration, int refreshTokenExpiration, string refreshToken)
            => RefreshToken(accessTokenExpiration, refreshTokenExpiration, refreshToken);
    }
}
