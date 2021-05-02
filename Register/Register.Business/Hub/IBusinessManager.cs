using Register.Business.Models;

namespace Register.Business.Hub
{
    public interface IBusinessManager
    {
        bool Login(RegisterDTO model);
        bool Register(RegisterDTO model);
    }
}
