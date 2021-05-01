using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RegisterBusiness.Hub;
using RegisterBusiness.Models;
using System;

namespace RegisterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IDistributedCache _cache;
        private readonly IBusiness _business;

        public RegisterController(ILogger<RegisterController> logger, IDistributedCache cache, IBusiness business)
        {
            _logger = logger;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
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
