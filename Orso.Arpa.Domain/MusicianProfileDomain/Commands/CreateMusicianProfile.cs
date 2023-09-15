using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfileDomain.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Commands
{
    public static class CreateMusicianProfile
    {
        public class Command : IRequest<MusicianProfile>, IHasInstrumentRequest
        {
            public byte LevelAssessmentInner { get; set; }
            public byte LevelAssessmentTeam { get; set; }
            public Guid PersonId { get; set; }
            public Guid InstrumentId { get; set; }
            public Guid? QualificationId { get; set; }
            public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }
            public MusicianProfileInquiryStatus? InquiryStatusTeam { get; set; }
            public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
            public IList<Guid> PreferredPositionsTeamIds { get; set; } = new List<Guid>();
            public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
            public IList<byte> PreferredPartsTeam { get; set; } = new List<byte>();
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                _ = RuleFor(c => c.InstrumentId)
                    .EntityExists<Command, Section>(arpaContext)
                    .MustAsync(async (command, instrumentId, cancellation) => !await arpaContext
                        .EntityExistsAsync<MusicianProfile>(mp => mp.InstrumentId == instrumentId && mp.PersonId == command.PersonId, cancellation))
                    .WithMessage("There is already a musician profile for this person with this instrument id");

                _ = RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                _ = RuleForEach(c => c.PreferredPositionsInnerIds)
                    .MusicianProfilePosition(arpaContext);

                _ = RuleForEach(c => c.PreferredPositionsTeamIds)
                    .MusicianProfilePosition(arpaContext);

                _ = RuleForEach(c => c.PreferredPartsInner)
                    .InstrumentPart(arpaContext);

                _ = RuleForEach(c => c.PreferredPartsTeam)
                    .InstrumentPart(arpaContext);
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
                var isFirstProfile = !await _arpaContext.EntityExistsAsync<MusicianProfile>(mp => mp.PersonId == request.PersonId, cancellationToken);
                var newMusicianProfile = new MusicianProfile(request, isFirstProfile);

                EntityEntry<MusicianProfile> createResult = _arpaContext.Add(newMusicianProfile);

                return await _arpaContext.SaveChangesAsync(cancellationToken) > 0
                    ? createResult.Entity
                    : throw new AffectedRowCountMismatchException(nameof(MusicianProfile));
            }
        }
    }
}
