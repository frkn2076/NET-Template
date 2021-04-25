using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RegisterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "HEY";
        }
    }
}
