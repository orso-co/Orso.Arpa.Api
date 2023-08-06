using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.MusicianProfileSections
{
    public static class Create
    {
        public class Command : ICreateCommand<MusicianProfileSection>
        {
            public Guid MusicianProfileId { get; set; }
            public Guid InstrumentId { get; set; }
            public byte LevelAssessmentInner { get; set; }
            public byte LevelAssessmentTeam { get; set; }
            public Guid? AvailabilityId { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicianProfileId)
                    .EntityExists<Command, MusicianProfile>(arpaContext);

                RuleFor(c => c.AvailabilityId)
                    .SelectValueMapping<Command, MusicianProfileSection>(arpaContext, a => a.InstrumentAvailability);

                RuleFor(c => c.InstrumentId)
                    .Cascade(CascadeMode.Stop)
                    .MustAsync(async (command, instrumentId, cancellation) => instrumentId != (await arpaContext.GetByIdAsync<MusicianProfile>(command.MusicianProfileId, cancellation)).InstrumentId)
                    .WithMessage("The doubling instrument may not be the main instrument")
                    .MustAsync(async (command, instrumentId, cancellation) => await IsValidDoublingInstrumentAsync(command.MusicianProfileId, instrumentId, arpaContext, cancellation))
                    .WithMessage("This instrument is no valid doubling instrument for the selected main instrument")
                    .MustAsync(async (command, instrumentId, cancellation) => !(await arpaContext
                        .EntityExistsAsync<MusicianProfileSection>(s =>
                            s.MusicianProfileId == command.MusicianProfileId
                            && s.SectionId == instrumentId, cancellation)))
                    .WithMessage("A doubling instrument with this instrument does already exist");
            }

            private static async Task<bool> IsValidDoublingInstrumentAsync(Guid musicianProfileId, Guid doublingInstrumentId, IArpaContext arpaContext, CancellationToken cancellationToken)
            {
                Section mainInstrument = (await arpaContext.GetByIdAsync<MusicianProfile>(musicianProfileId, cancellationToken)).Instrument;
                if (mainInstrument.IsInstrument)
                {
                    return mainInstrument.Children.Select(c => c.Id).Contains(doublingInstrumentId);
                }
                if (mainInstrument.ParentId == doublingInstrumentId)
                {
                    return true;
                }
                return mainInstrument.Parent.Children.Select(c => c.Id).Contains(doublingInstrumentId);
            }
        }
    }
}
