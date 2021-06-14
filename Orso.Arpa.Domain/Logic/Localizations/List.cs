using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Localizations
{
    public static class List
    {

        public class Query : IRequest<IEnumerable<Entities.Localization>>
        {
            public string Culture { get; init; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Entities.Localization>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<IEnumerable<Entities.Localization>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                IList<Entities.Localization> localizations = _arpaContext.Localizations
                    .AsQueryable().Where(e =>
                        e.LocalizationCulture.Equals(request.Culture)).ToList();

                return Task.FromResult((IEnumerable<Entities.Localization>) localizations);
            }
        }
    }
}