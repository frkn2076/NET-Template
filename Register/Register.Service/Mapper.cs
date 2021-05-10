using Infra.CommonMessages;
using Mapster;
using Register.Business.Models;
using Register.Service.ViewModels;

namespace Register.Service
{
    public class Mapper
    {

        public static void MapsterInit()
        {
            TypeAdapterConfig<RegisterRequest, RegisterDTORequest>.NewConfig();
            //.Map(dest => dest.Name, src => src.Name + " " + src.Password);

            TypeAdapterConfig<TokenDTOResponse, BaseResponse>.NewConfig();
                //.Map(dest => dest.AccessToken, src => src.AccessToken)
                //.Map(dest => dest.AccessTokenExpiresIn, src => src.AccessTokenExpiresIn)
                //.Map(dest => dest.RefreshToken, src => src.RefreshToken)
                //.Map(dest => dest.RefreshTokenExpiresIn, src => src.RefreshTokenExpiresIn);
        }
    }
}
