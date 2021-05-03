using Register.Business.Models;
using Register.Repository;

namespace Register.Business.Hub.Implementation
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IRegisterRepo _registerRepo;
        public BusinessManager(IRegisterRepo registerRepo) => _registerRepo = registerRepo;

        private bool Login(RegisterDTORequest model)
        {
            return _registerRepo.HasAny(model.Name, model.Password);
        }

        private RegisterDTOResponse Register(RegisterDTORequest model)
        {
            var isExist = _registerRepo.IsExist(model.Name);
            if (isExist)
                return RegisterDTOResponse.AlreadyExists;

            _registerRepo.Insert(model.Name, model.Password);
            var isSuccess = _registerRepo.SaveChangesAsync().Result > 0;
            return isSuccess ? RegisterDTOResponse.Success : RegisterDTOResponse.Fail;
        }

        bool IBusinessManager.Login(RegisterDTORequest model) => Login(model);
        RegisterDTOResponse IBusinessManager.Register(RegisterDTORequest model) => Register(model);
    }
}
