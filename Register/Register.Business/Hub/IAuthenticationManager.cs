using Register.Business.Models;

namespace Register.Business.Hub
{
    public interface IAuthenticationManager
    {
        TokenDTOResponse GenerateToken(int accessTokenExpiration, int refreshTokenExpiration, params (string, string)[] claims);
        TokenDTOResponse RefreshToken(int accessTokenExpiration, int refreshTokenExpiration, string refreshToken);
    }
}
