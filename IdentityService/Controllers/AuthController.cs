using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using TokenManager;
using TokenManager.Models;

namespace IdentityService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IDistributedCache _redisCache;

        public AuthController(ILogger<AuthController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("CreateToken")]
        public string Post(User user)
        {
            //_redisCache.SetString("name", "Holaaaa");
            return Manager.CreateToken(new User() { Name = "Furkan", Role = RoleType.Normal });
        }

        //[HttpGet("ValidateToken")]
        //public string Get()
        //{
        //    var claims = HttpContext.User.Claims.ToDictionary(x => x.Type, x => x.Value);
        //    var name = claims[ClaimTypes.Name];
        //    var role = claims[ClaimTypes.Role];
        //    return $"{role} {name}";
        //}
    }
}
