using Register.Business.Models;
using Register.Repository;

namespace Register.Business.Hub.Implementation
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IRegisterRepo _registerRepo;
        public BusinessManager(IRegisterRepo registerRepo) => _registerRepo = registerRepo;

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

        bool IBusinessManager.Login(RegisterDTO model) => Login(model);
        bool IBusinessManager.Register(RegisterDTO model) => Register(model);
    }
}
