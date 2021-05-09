using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Dummy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DummyController : ControllerBase
    {
        private readonly ILogger<DummyController> _logger;

        public DummyController(ILogger<DummyController> logger)
        {
            _logger = logger;
        }

        [HttpGet("dummy")]
        public string Get()
        {
            var a = User.Claims.ToList();
            return "Success";
        }
    }
}
