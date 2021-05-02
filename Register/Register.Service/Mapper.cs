using Mapster;
using Register.Business.Models;
using Register.Service.ViewModels;

namespace Register.Service
{
    public class Mapper
    {
        public static void MapsterInit()
        {
            TypeAdapterConfig<RegisterViewModel, RegisterDTO>.NewConfig()
                            .Map(dest => dest.Name, src => src.Name /*+ " " + src.Password*/);
        }
    }
}
