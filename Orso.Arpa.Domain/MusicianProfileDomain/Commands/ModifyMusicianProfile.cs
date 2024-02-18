using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.Logic.MusicianProfileDomain.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Commands
{
    public static class ModifyMusicianProfile
    {
        public class Command : IRequest, IHasInstrumentRequest
        {
            public Guid Id { get; set; }
            public Guid InstrumentId { get; set; }

            public bool IsMainProfile { get; set; }

            public byte LevelAssessmentInner { get; set; }
            public byte LevelAssessmentTeam { get; set; }
            public byte ProfilePreferenceInner { get; set; }
            public byte ProfilePreferenceTeam { get; set; }

            public string BackgroundInner { get; set; }
            public string BackgroundTeam { get; set; }
            public string SalaryComment { get; set; }
            public Guid QualificationId { get; set; }

            public Guid? SalaryId { get; set; }

            public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }

            public MusicianProfileInquiryStatus? InquiryStatusTeam { get; set; }

            public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
            public IList<Guid> PreferredPositionsTeamIds { get; set; } = new List<Guid>();
            public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
            public IList<byte> PreferredPartsTeam { get; set; } = new List<byte>();
            public MusicianProfile ExistingMusicianProfile { get; set; }
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

                _ = RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                _ = RuleFor(c => c.SalaryId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Salary);

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
                    MusicianProfile mainProfile = await _arpaContext
                        .Set<MusicianProfile>()
                        .AsQueryable()
                        .SingleOrDefaultAsync(mp => mp.PersonId == existingMusicianProfile.PersonId && mp.IsMainProfile, cancellationToken);
                    mainProfile.TurnOffIsMainProfileFlag();
                    _ = _arpaContext.Set<MusicianProfile>().Update(mainProfile);
                }

                existingMusicianProfile.Update(request);
                _arpaContext.Entry(existingMusicianProfile)?.CurrentValues?.SetValues(existingMusicianProfile);

                UpdatePreferredPositionsInner(existingMusicianProfile.PreferredPositionsInner, request.PreferredPositionsInnerIds, existingMusicianProfile.Id);
                UpdatePreferredPositionsTeam(existingMusicianProfile.PreferredPositionsTeam, request.PreferredPositionsTeamIds, existingMusicianProfile.Id);

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

            private void UpdatePreferredPositionsTeam(ICollection<MusicianProfilePositionTeam> collectionToUpdate, IList<Guid> updateList, Guid musicianProfileId)
            {
                foreach (MusicianProfilePositionTeam position in collectionToUpdate)
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
                        _ = _arpaContext.Add(new MusicianProfilePositionTeam(selectValueSectionId, musicianProfileId));
                    }
                }
            }
        }
    }
}
