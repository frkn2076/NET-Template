using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gateway.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly ILogger<GatewayController> _logger;

        public GatewayController(ILogger<GatewayController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return string.Empty;
        }
    }
}
