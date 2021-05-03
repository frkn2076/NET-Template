using Register.Business.Models;

namespace Register.Business.Hub
{
    public interface IBusinessManager
    {
        bool Login(RegisterDTORequest model);
        RegisterDTOResponse Register(RegisterDTORequest model);
    }
}
