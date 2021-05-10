using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Notifier.Service
{
    public class NotifierService : Notifier.NotifierBase
    {
        private readonly ILogger<NotifierService> _logger;
        public NotifierService(ILogger<NotifierService> logger)
        {
            _logger = logger;
        }

        public override Task<NotifierResponse> Push(NotifierRequest request, ServerCallContext context)
        {

            return Task.FromResult(new NotifierResponse
            {
                IsSuccess = true
            });
        }
    }
}
