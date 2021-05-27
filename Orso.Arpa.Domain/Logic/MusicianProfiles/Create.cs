using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public static class Create
    {
        public class Command : IRequest<MusicianProfile>
        {
            public byte LevelAssessmentPerformer { get; set; }
            public byte LevelAssessmentStaff { get; set; }
            public Guid PersonId { get; set; }
            public Guid InstrumentId { get; set; }
            public Guid? QualificationId { get; set; }
            public Guid? InquiryStatusPerformerId { get; set; }
            public Guid? InquiryStatusStaffId { get; set; }
            public IList<DoublingInstrumentCommand> DoublingInstruments { get; set; } = new List<DoublingInstrumentCommand>();
            public IList<Guid> PreferredPositionsPerformerIds { get; set; } = new List<Guid>();
            public IList<Guid> PreferredPositionsStaffIds { get; set; } = new List<Guid>();
            //public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
            //public IList<PreferredPart> PreferredPartsStaff { get; set; } = new List<PreferredPart>();
        }

        public class DoublingInstrumentCommand
        {
            public Guid InstrumentId { get; set; }
            public byte LevelAssessmentPerformer { get; set; }
            public byte LevelAssessmentStaff { get; set; }
            public Guid? AvailabilityId { get; set; }
            public string Comment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext, nameof(Command.PersonId));

                RuleFor(c => c.InstrumentId)
                    .EntityExists<Command, Section>(arpaContext, nameof(Command.InstrumentId))
                    .MustAsync(async (command, instrumentId, cancellation) => !await arpaContext
                        .EntityExistsAsync<MusicianProfile>(mp => mp.InstrumentId == instrumentId && mp.PersonId == command.PersonId, cancellation))
                    .WithMessage("There is already a musician profile for this person with this instrument id");

                RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                RuleFor(c => c.InquiryStatusPerformerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusPerformer);

                RuleFor(c => c.InquiryStatusStaffId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusStaff);

                RuleForEach(c => c.DoublingInstruments)
                    .SetValidator(c => new DoublingInstrumentValidator(arpaContext) { MainInstrumentId = c.InstrumentId });

                RuleForEach(c => c.PreferredPositionsPerformerIds)
                    .SelectValueSection(arpaContext, nameof(Command.PreferredPositionsPerformerIds));

                RuleForEach(c => c.PreferredPositionsStaffIds)
                    .SelectValueSection(arpaContext, nameof(Command.PreferredPositionsStaffIds));

                //ToDo Validation for Collections
            }
        }

        public class DoublingInstrumentValidator : AbstractValidator<DoublingInstrumentCommand>
        {
            public Guid MainInstrumentId { get; set; }
            public DoublingInstrumentValidator(IArpaContext arpaContext)
            {
                RuleFor(c => c.AvailabilityId)
                    .SelectValueMapping<DoublingInstrumentCommand, MusicianProfileSection>(arpaContext, a => a.InstrumentAvailability);

                RuleFor(c => c.InstrumentId)
                    .Must(instrumentId => instrumentId != MainInstrumentId)
                    .WithMessage("The doubling instrument may not be the main instrument")
                    .MustAsync(async (command, instrumentId, cancellation) => await IsValidDoublingInstrumentAsync(MainInstrumentId, instrumentId, arpaContext, cancellation))
                    .WithMessage("This instrument is no valid doubling instrument for the selected main instrument");
            }

            private async Task<bool> IsValidDoublingInstrumentAsync(Guid mainInstrumentId, Guid doublingInstrumentId, IArpaContext arpaContext, CancellationToken cancellationToken)
            {
                Section mainInstrument = await arpaContext.FindAsync<Section>(new object[] { mainInstrumentId }, cancellationToken);
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

        public class Handler : IRequestHandler<Command, MusicianProfile>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<MusicianProfile> Handle(Command request, CancellationToken cancellationToken)
            {
                var isFirstProfile = !(await _arpaContext.EntityExistsAsync<MusicianProfile>(mp => mp.PersonId == request.PersonId, cancellationToken));
                var newMusicianProfile = new MusicianProfile(request, isFirstProfile);

                EntityEntry<MusicianProfile> createResult = _arpaContext.Add(newMusicianProfile);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return createResult.Entity;
                }

                throw new Exception($"Problem creating {nameof(MusicianProfile)}");
            }
        }
    }
}
