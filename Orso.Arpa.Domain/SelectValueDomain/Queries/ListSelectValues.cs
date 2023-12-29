using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Queries
{
    public static class ListSelectValues
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
                    .AsQueryable()
                    .Where(c =>
#pragma warning disable RCS1155, CA1862 // Use StringComparison when comparing strings. -> ToLower() is used to allow ef core to perform the query on db server
                        c.Table.ToLower() == request.TableName.ToLower()
                        && c.Property.ToLower() == request.PropertyName.ToLower())
#pragma warning restore RCS1155, CA1862 // Use StringComparison when comparing strings.
                    .SelectMany(c => c.SelectValueMappings)
                    .OrderBy(s => s.SortOrder ?? int.MaxValue)
                    .ThenBy(s => s.SelectValue.Name)
                    .ToImmutableList() as IImmutableList<SelectValueMapping>);
            }
        }
    }
}
