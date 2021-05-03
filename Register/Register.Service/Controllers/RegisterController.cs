using Infra.CommonMessages;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Register.Business.Hub;
using Register.Business.Models;
using Register.Service.ViewModels;

namespace RegisterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IBusinessManager _business;

        public RegisterController(IBusinessManager business) => _business = business;

        [AllowAnonymous]
        [HttpPost("Login")]
        public BaseResponse Login(RegisterRequest register)
        {
            var model = register.Adapt<RegisterDTORequest>();
            var isSuccess = _business.Login(model);
            return isSuccess ? BaseResponse.Success : BaseResponse.Fail;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public BaseResponse Register(RegisterRequest register)
        {
            var model = register.Adapt<RegisterDTORequest>();
            var response = _business.Register(model);
            switch (response)
            {
                case RegisterDTOResponse.Success:
                    return BaseResponse.Success;
                case RegisterDTOResponse.AlreadyExists:
                    return BaseResponse.Create("User already exists");
                default:
                    return BaseResponse.Fail;
            }
        }
    }
}
