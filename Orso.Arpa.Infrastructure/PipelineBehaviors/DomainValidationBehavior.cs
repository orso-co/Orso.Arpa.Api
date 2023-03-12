using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Misc.Extensions;

namespace Orso.Arpa.Infrastructure.PipelineBehaviors
{
    public class DomainValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public DomainValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = (await _validators
                .SelectAsync(async v => await v.ValidateAsync(context)))
                .SelectMany(x => x.Errors)
                .Where(e => e != null)
                .ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
