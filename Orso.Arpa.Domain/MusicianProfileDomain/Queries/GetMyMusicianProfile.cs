using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Queries
{
    public static class GetMyMusicianProfile
    {

        public class Query : IRequest<MusicianProfile>
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }

        public class Handler : IRequestHandler<Query, MusicianProfile>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<MusicianProfile> Handle(Query request, CancellationToken cancellationToken)
            {
                MusicianProfile musicianProfile = await _arpaContext
                    .Set<MusicianProfile>()
                    .SingleOrDefaultAsync(
                        mp => mp.PersonId == request.PersonId && mp.Id == request.Id, cancellationToken) 
                        ?? throw new NotFoundException(nameof(MusicianProfile), nameof(Query.Id));

                return musicianProfile;
            }
        }
    }
}
