using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("1")]
        public string Get()
        {
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
    }
}
