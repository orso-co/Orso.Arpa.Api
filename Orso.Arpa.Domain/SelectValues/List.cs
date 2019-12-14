using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.SelectValues
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<SelectValue>>
        {
            public string TableName { get; set; }
            public string PropertyName { get; set; }
        }

        public class Handler : IRequestHandler<Query, IImmutableList<SelectValue>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public Task<IImmutableList<SelectValue>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository
                    .GetAll<SelectValueCategory>()
                    .Where(c =>
                        c.Table.Equals(request.TableName, StringComparison.InvariantCultureIgnoreCase)
                        && c.Property.Equals(request.PropertyName, StringComparison.InvariantCultureIgnoreCase))
                    .SelectMany(c => c.SelectValueMappings)
                    .Select(m => m.SelectValue)
                    .ToImmutableList() as IImmutableList<SelectValue>);
            }
        }
    }
}
