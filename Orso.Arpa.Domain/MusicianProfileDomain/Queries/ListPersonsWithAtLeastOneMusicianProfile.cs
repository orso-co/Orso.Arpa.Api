using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Queries
{
    public static class ListPersonsWithAtLeastOneMusicianProfile
    {
        public class Query : IRequest<IOrderedQueryable<Person>> { }

        public class Handler : IRequestHandler<Query, IOrderedQueryable<Person>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<IOrderedQueryable<Person>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_arpaContext
                    .Set<Person>()
                    .AsQueryable()
                    .Where(p => p.MusicianProfiles.Count > 0)
                    .OrderBy(p => p.Surname)
                    .ThenBy(p => p.GivenName));
            }
        }
    }
}
