using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class RemoveRegionPreference
    {
        public class Command : IRequest
        {
            public Guid MusicianProfileId { get; set; }
            public Guid RegionPreferenceId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.RegionPreferenceId)
                    .MustAsync(async (cmd, regionPreferenceId, cancellation) => await arpaContext
                        .Set<RegionPreference>()
                        .AnyAsync(ar => ar.Id == regionPreferenceId && ar.MusicianProfileId == cmd.MusicianProfileId, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Region preference could not be found.");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                RegionPreference regionPreferenceToRemove = await _arpaContext
                    .GetByIdAsync<RegionPreference>(request.RegionPreferenceId, cancellationToken);

                _arpaContext.Set<RegionPreference>().Remove(regionPreferenceToRemove);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem removing region preference");
            }
        }
    }
}
