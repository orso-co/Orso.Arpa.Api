using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.SelectValues
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<SelectValueMapping>>
        {
            public string TableName { get; set; }
            public string PropertyName { get; set; }
        }

        public class Handler : IRequestHandler<Query, IImmutableList<SelectValueMapping>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public async Task<IImmutableList<SelectValueMapping>> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _repository
                    .GetAll<SelectValueCategory>()
                    .ToListAsync())
                    .Where(c =>
                        c.Table.Equals(request.TableName, StringComparison.InvariantCultureIgnoreCase)
                        && c.Property.Equals(request.PropertyName, StringComparison.InvariantCultureIgnoreCase))
                    .SelectMany(c => c.SelectValueMappings)
                    .OrderBy(s => s.SelectValue.Name)
                    .ToImmutableList() as IImmutableList<SelectValueMapping>;
            }
        }
    }
}
