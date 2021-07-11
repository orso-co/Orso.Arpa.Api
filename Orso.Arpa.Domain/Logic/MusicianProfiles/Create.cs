using System;
using System.Collections.Generic;
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
        public class Command : IRequest<MusicianProfile>, IHasInstrumentRequest
        {
            public byte LevelAssessmentInner { get; set; }
            public byte LevelAssessmentTeam { get; set; }
            public Guid PersonId { get; set; }
            public Guid InstrumentId { get; set; }
            public Guid? QualificationId { get; set; }
            public Guid? InquiryStatusInnerId { get; set; }
            public Guid? InquiryStatusTeamId { get; set; }
            public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
            public IList<Guid> PreferredPositionsTeamIds { get; set; } = new List<Guid>();
            public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
            public IList<byte> PreferredPartsTeam { get; set; } = new List<byte>();
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.InstrumentId)
                    .EntityExists<Command, Section>(arpaContext)
                    .MustAsync(async (command, instrumentId, cancellation) => !await arpaContext
                        .EntityExistsAsync<MusicianProfile>(mp => mp.InstrumentId == instrumentId && mp.PersonId == command.PersonId, cancellation))
                    .WithMessage("There is already a musician profile for this person with this instrument id");

                RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                RuleFor(c => c.InquiryStatusInnerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusInner);

                RuleFor(c => c.InquiryStatusTeamId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusTeam);

                RuleForEach(c => c.PreferredPositionsInnerIds)
                    .MusicianProfilePosition(arpaContext);

                RuleForEach(c => c.PreferredPositionsTeamIds)
                    .MusicianProfilePosition(arpaContext);

                RuleForEach(c => c.PreferredPartsInner)
                    .InstrumentPart(arpaContext);

                RuleForEach(c => c.PreferredPartsTeam)
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
