using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.PipelineBehaviors
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IUserAccessor _userAccessor;

        public RequestLogger(ILogger<TRequest> logger, IUserAccessor userAccessor)
        {
            _logger = logger;
            _userAccessor = userAccessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            string displayName = _userAccessor.DisplayName;

            _logger.LogInformation("Orso.Arpa.Domain Request: {Name} {@DisplayName} {@Request}",
                requestName, displayName, request);

            return Task.CompletedTask;
        }
    }
}
