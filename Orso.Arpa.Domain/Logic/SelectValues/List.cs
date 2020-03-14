using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<IImmutableList<SelectValueMapping>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_arpaContext
                    .SelectValueCategories
                    .Where(c =>
                        c.Table.ToLower() == request.TableName.ToLower()
                        && c.Property.ToLower() == request.PropertyName.ToLower())
                    .SelectMany(c => c.SelectValueMappings)
                    .OrderBy(s => s.SelectValue.Name)
                    .ToImmutableList() as IImmutableList<SelectValueMapping>);
            }
        }
    }
}
