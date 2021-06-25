using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Internal;
using MediatR;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Localizations
{
    public class Modify
    {
        private static readonly object DbLock = new();

        public class Query : IRequest
        {
            public IList<Entities.Localization> Localizations { get; init; }

            public string Culture { get; init; }
        }

        public class Handler : IRequestHandler<Query>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<Unit> Handle(Query request,
                CancellationToken cancellationToken)
            {
                lock (DbLock)
                {
                    var dbEntries = _arpaContext.Localizations.AsQueryable().Where(q => q.LocalizationCulture.Equals(request.Culture)).ToList();
                    dbEntries.ForAll(dbEntry =>
                    {
                            Entities.Localization result = request.Localizations.Count == 0
                                ? null
                                : request.Localizations.AsQueryable()
                                .DefaultIfEmpty(null).FirstOrDefault(l =>
                                    l.ResourceKey.Equals(dbEntry.ResourceKey) &&
                                    l.Key.Equals(dbEntry.Key));
                            if (result == null)
                            {
                                _arpaContext.Localizations.Remove(dbEntry);
                            }
                            else
                            {
                                if (!result.Text.Equals(dbEntry.Text))
                                {
                                    dbEntry.Text = result.Text;
                                }
                                request.Localizations.Remove(result);
                            }
                    });

                    request.Localizations.ForAll(l =>
                    {
                        _arpaContext.Localizations.Add(l);
                    });

                     _arpaContext.SaveChangesAsync(cancellationToken);
                }
                return Task.FromResult(Unit.Value);
            }
        }
    }
}
