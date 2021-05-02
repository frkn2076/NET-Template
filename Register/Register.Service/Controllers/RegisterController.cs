using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Register.Business.Hub;
using Register.Business.Models;
using Register.Service.ViewModels;

namespace RegisterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IBusinessManager _business;

        public RegisterController(ILogger<RegisterController> logger, IBusinessManager business)
        {
            _logger = logger;
            _business = business;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public bool Login(RegisterViewModel register)
        {
            var model = register.Adapt<RegisterDTO>();
            var isSuccess = _business.Login(model);
            return isSuccess;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public bool Register(RegisterViewModel register)
        {
            var model = register.Adapt<RegisterDTO>();
            var isSuccess = _business.Register(model);
            return isSuccess;
        }
    }
}
