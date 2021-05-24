using System;
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
            //public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
            //public IList<PreferredPosition> PreferredPositionsPerformer { get; set; } = new List<PreferredPosition>();
            //public IList<PreferredPosition> PreferredPositionsStaff { get; set; } = new List<PreferredPosition>();
            //public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
            //public IList<PreferredPart> PreferredPartsStaff { get; set; } = new List<PreferredPart>();
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

                //ToDo Validation for Collections
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
