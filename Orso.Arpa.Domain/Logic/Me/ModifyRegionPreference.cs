using System;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class ModifyRegionPreference
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
