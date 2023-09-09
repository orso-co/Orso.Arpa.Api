using System;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.RegionDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.RegionDomain.Commands
{
    public static class ModifyMyRegionPreference
    {
        public class Command : IModifyCommand<RegionPreference>
        {
            public Guid Id { get; set; }
            public Guid MusicianProfileId { get; set; }

            public byte Rating { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .MustAsync(async (cmd, regionPreferenceId, cancellation) => await arpaContext
                        .Set<RegionPreference>()
                        .AnyAsync(ar => ar.Id == regionPreferenceId && ar.MusicianProfileId == cmd.MusicianProfileId, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Region preference could not be found.");
            }
        }
    }
}
