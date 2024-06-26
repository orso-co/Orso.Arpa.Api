using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Infrastructure.PipelineBehaviors
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<RequestPerformanceBehaviour<TRequest, TResponse>> _logger;
        private readonly IUserAccessor _userAccessor;

        public RequestPerformanceBehaviour(
            ILogger<RequestPerformanceBehaviour<TRequest, TResponse>> logger,
            IUserAccessor userAccessor)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            TResponse response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).FullName;
                var displayName = _userAccessor.DisplayName;

                _logger.LogWarning("Orso.Arpa.Domain Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds)|User: {DisplayName}|Request-Object: {@Request}",
                    requestName, elapsedMilliseconds, displayName, request);
            }

            return response;
        }
    }
}
