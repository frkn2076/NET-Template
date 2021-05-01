using RegisterBusiness.Models;
using RegisterRepository;

namespace RegisterBusiness.Hub.Implementation
{
    public class Business : IBusiness
    {
        private readonly IRegisterRepo _registerRepo;
        public Business(IRegisterRepo registerRepo) => _registerRepo = registerRepo;

        private bool Login(RegisterDTO model)
        {
            return _registerRepo.HasAny(model.Name, model.Password);
        }

        private bool Register(RegisterDTO model)
        {
            _registerRepo.Insert(model.Name, model.Password);
            var isRegistered = _registerRepo.SaveChangesAsync().Result > 0;
            return isRegistered;
        }

        bool IBusiness.Login(RegisterDTO model) => Login(model);
        bool IBusiness.Register(RegisterDTO model) => Register(model);
    }
}
