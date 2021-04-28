using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;

namespace RegisterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IDistributedCache _redisCache;

        public RegisterController(ILogger<RegisterController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("1")]
        public string Get()
        {
            var res = _redisCache.GetString("name");
            return res;
        }
    }
}
