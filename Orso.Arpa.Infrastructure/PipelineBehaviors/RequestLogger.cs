using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Infrastructure.PipelineBehaviors
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IUserAccessor _userAccessor;

        public RequestLogger(ILogger<RequestLogger<TRequest>> logger, IUserAccessor userAccessor)
        {
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).FullName;
            string displayName = _userAccessor.DisplayName;

            _logger.LogInformation("Orso.Arpa.Domain Request sent: {Name}|User: {@DisplayName}|Request-Object: {@Request}",
                requestName, displayName, request);

            return Task.CompletedTask;
        }
    }
}
