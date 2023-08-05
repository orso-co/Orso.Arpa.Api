using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class ModifyMusicianProfile
    {
        public class Command : IRequest, IHasInstrumentRequest
        {
            public Guid Id { get; set; }
            public Guid InstrumentId { get; set; }

            public bool IsMainProfile { get; set; }

            public byte LevelAssessmentInner { get; set; }
            public byte ProfilePreferenceInner { get; set; }

            public string BackgroundInner { get; set; }

            public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
            public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
            public MusicianProfile ExistingMusicianProfile { get; set; }
            public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(c => c.ExistingMusicianProfile)
                    .NotEmpty();

                _ = RuleFor(c => c.IsMainProfile)
                    .Must((command, isMainProfile) => isMainProfile || !command.ExistingMusicianProfile.IsMainProfile)
                    .WithMessage("You may not turn off the IsMainProfile flag");

                _ = RuleForEach(c => c.PreferredPositionsInnerIds)
                    .MusicianProfilePosition(arpaContext);

                _ = RuleForEach(c => c.PreferredPartsInner)
                    .InstrumentPart(arpaContext);
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
                MusicianProfile existingMusicianProfile = request.ExistingMusicianProfile;

                if (request.IsMainProfile && !existingMusicianProfile.IsMainProfile)
                {
                    MusicianProfile mainProfile = await _arpaContext.MusicianProfiles.AsQueryable().SingleOrDefaultAsync(mp => mp.PersonId == existingMusicianProfile.PersonId && mp.IsMainProfile, cancellationToken);
                    mainProfile.TurnOffIsMainProfileFlag();
                    _ = _arpaContext.MusicianProfiles.Update(mainProfile);
                }

                existingMusicianProfile.Update(request);
                _arpaContext.Entry(existingMusicianProfile)?.CurrentValues?.SetValues(existingMusicianProfile);

                UpdatePreferredPositionsInner(existingMusicianProfile.PreferredPositionsInner, request.PreferredPositionsInnerIds, existingMusicianProfile.Id);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(MusicianProfile));
            }

            private void UpdatePreferredPositionsInner(ICollection<MusicianProfilePositionInner> collectionToUpdate, IList<Guid> updateList, Guid musicianProfileId)
            {
                foreach (MusicianProfilePositionInner position in collectionToUpdate)
                {
                    if (!updateList.Contains(position.SelectValueSectionId))
                    {
                        _ = _arpaContext.Remove(position);
                    }
                }

                if (updateList.Count == 0)
                {
                    return;
                }

                IEnumerable<Guid> existingSelectValueSectionIds = collectionToUpdate.Select(p => p.SelectValueSectionId);
                foreach (Guid selectValueSectionId in updateList)
                {
                    if (!existingSelectValueSectionIds.Contains(selectValueSectionId))
                    {
                        _ = _arpaContext.Add(new MusicianProfilePositionInner(selectValueSectionId, musicianProfileId));
                    }
                }
            }
        }
    }
}
