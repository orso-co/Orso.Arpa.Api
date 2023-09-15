using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.LocalizationDomain.Model;

namespace Orso.Arpa.Domain.LocalizationDomain.Queries
{
    public static class ListLocalizations
    {

        public class Query : IRequest<IEnumerable<Localization>>
        {
            public string Culture { get; init; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Localization>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<IEnumerable<Localization>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                return await _arpaContext.Localizations
                    .AsQueryable().Where(e =>
                        e.LocalizationCulture.Equals(request.Culture)).ToListAsync(cancellationToken);
            }
        }
    }
}
