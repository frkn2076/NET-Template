using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

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

        [AllowAnonymous]
        [HttpGet("1")]
        public string Get()
        {
            _redisCache.SetString("name", "Holaaaa");
            return TokenService.CreateToken(new User() { Name = "Furkan", Role = RoleType.Normal });
        }

        [HttpGet("2")]
        public string Gett()
        {
            var claims = HttpContext.User.Claims.ToDictionary(x => x.Type, x => x.Value);
            var name = claims[ClaimTypes.Name];
            var role = claims[ClaimTypes.Role];
            return $"{role} {name}";
        }

        [AllowAnonymous]
        [HttpPost("2")]
        public string Post(Mode name)
        {
            return "Furkan";
        }
    }

    public class Mode
    {
        public Mode()
        {

        }
        public string email { get; set; }
        public string password { get; set; }
    }
}
