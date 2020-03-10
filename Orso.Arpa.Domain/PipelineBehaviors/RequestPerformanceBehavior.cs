using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.PipelineBehaviors
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IUserAccessor _userAccessor;

        public RequestPerformanceBehaviour(
            ILogger<TRequest> logger,
            IUserAccessor userAccessor)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            TResponse response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var displayName = _userAccessor.DisplayName;

                _logger.LogWarning("Orso.Arpa.Domain Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@DisplayName} {@Request}",
                    requestName, elapsedMilliseconds, displayName, request);
            }

            return response;
        }
    }
}
