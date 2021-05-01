using Mapster;
using System;

namespace Mapper
{
    public class Mapper
    {
        public void MapperInit()
        {
            TypeAdapterConfig<Employee, EmployeeViewModel>.NewConfig()
                            .Map(dest => dest.Name, src => src.FirstName + " " + src.FamilyName)
                            .Map()
        }
    }
}
