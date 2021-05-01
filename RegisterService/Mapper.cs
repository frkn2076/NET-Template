using Mapster;
using RegisterBusiness.Models;

namespace RegisterService
{
    public class Mapper
    {
        public static void MapsterInit()
        {
            //TypeAdapterConfig<RegisterViewModel, RegisterDTO>.NewConfig();
            TypeAdapterConfig<RegisterViewModel, RegisterDTO>.NewConfig()


                            .Map(dest => dest.Name, src => src.Name + " " + src.Password);
        }
    }
}
