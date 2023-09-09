using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.RegionDomain.Enums;
using Orso.Arpa.Domain.RegionDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.RegionDomain.Commands
{
    public static class CreateMyRegionPreference
    {
        public class Command : ICreateCommand<RegionPreference>
        {
            public Guid MusicianProfileId { get; set; }
            public byte Rating { get; set; }
            public Guid RegionId { get; set; }
            public string Comment { get; set; }
            public RegionPreferenceType Type { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicianProfileId)
                    .MustAsync(async (command, musicianProfileId, cancellation) => !(await arpaContext
                        .EntityExistsAsync<RegionPreference>(p => p.MusicianProfileId == musicianProfileId && p.RegionId == command.RegionId && p.Type == command.Type, cancellation)))
                    .WithMessage("The region preference does already exist");

                RuleFor(c => c.RegionId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Region>(arpaContext)
                    .MustAsync(async (command, regionId, cancellation) =>
                    {
                        Region region = await arpaContext.GetByIdAsync<Region>(regionId, cancellation);
                        return command.Type switch
                        {
                            RegionPreferenceType.Rehearsal => region.IsForRehearsal,
                            RegionPreferenceType.Performance => region.IsForPerformance,
                            _ => false,
                        };
                    })
                    .WithMessage("This region is not allowed for the supplied region preference type");
            }
        }
    }
}
