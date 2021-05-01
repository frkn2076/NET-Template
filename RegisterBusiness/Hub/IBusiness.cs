using RegisterBusiness.Models;

namespace RegisterBusiness.Hub
{
    public interface IBusiness
    {
        bool Login(RegisterDTO model);
        bool Register(RegisterDTO model);
    }
}
