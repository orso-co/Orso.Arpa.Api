using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
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
            public bool IsDeactivated { get; set; }

            public byte LevelAssessmentInner { get; set; }
            public byte ProfilePreferenceInner { get; set; }

            public string BackgroundInner { get; set; }

            public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
            public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
            public MusicianProfile ExistingMusicianProfile { get; set; }
            public Guid? InquiryStatusInnerId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.ExistingMusicianProfile)
                    .NotEmpty();

                RuleFor(c => c.IsMainProfile)
                    .Must((command, isMainProfile) => isMainProfile || (!isMainProfile && !command.ExistingMusicianProfile.IsMainProfile))
                    .WithMessage("You may not turn off the IsMainProfile flag");

                RuleForEach(c => c.PreferredPositionsInnerIds)
                    .MusicianProfilePosition(arpaContext);

                RuleForEach(c => c.PreferredPartsInner)
                    .InstrumentPart(arpaContext);

                RuleFor(c => c.InquiryStatusInnerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusInner);

                RuleFor(c => c.IsDeactivated)
                    .Must((command, isDeactivated, context) => !isDeactivated || command.ExistingMusicianProfile.ProjectParticipations.Select(pp => pp.Project).All(p => p.IsCompleted))
                    .WithMessage("You may not deactivate a musician profile which is participating in an active project");
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, MusicianProfile>()
                    .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.IsMainProfile))
                    .ForMember(dest => dest.IsDeactivated, opt => opt.MapFrom(src => src.IsDeactivated))

                    .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.LevelAssessmentInner))
                    .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.ProfilePreferenceInner))

                    .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.BackgroundInner))

                    .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.PreferredPartsInner))
                    .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.InquiryStatusInnerId))

                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(IArpaContext arpaContext, IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                MusicianProfile existingMusicianProfile = request.ExistingMusicianProfile;

                if (request.IsMainProfile && !existingMusicianProfile.IsMainProfile)
                {
                    MusicianProfile mainProfile = await _arpaContext.MusicianProfiles.AsQueryable().SingleOrDefaultAsync(mp => mp.PersonId == existingMusicianProfile.PersonId && mp.IsMainProfile, cancellationToken);
                    mainProfile.TurnOffIsMainProfileFlag();
                    _arpaContext.MusicianProfiles.Update(mainProfile);
                }

                MusicianProfile modifiedEntity = _mapper.Map(request, existingMusicianProfile);
                _arpaContext.Entry(existingMusicianProfile)?.CurrentValues?.SetValues(modifiedEntity);

                UpdatePreferredPositionsInner(modifiedEntity.PreferredPositionsInner, request.PreferredPositionsInnerIds, modifiedEntity.Id);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new Exception("Problem updating musician profile");
            }

            private void UpdatePreferredPositionsInner(ICollection<MusicianProfilePositionInner> collectionToUpdate, IList<Guid> updateList, Guid musicianProfileId)
            {
                if (updateList.Count == 0)
                {
                    collectionToUpdate.Clear();
                    return;
                }

                foreach (MusicianProfilePositionInner position in collectionToUpdate)
                {
                    if (!updateList.Contains(position.SelectValueSectionId))
                    {
                        _arpaContext.Remove(position);
                    }
                }

                IEnumerable<Guid> existingSelectValueSectionIds = collectionToUpdate.Select(p => p.SelectValueSectionId);
                foreach (Guid selectValueSectionId in updateList)
                {
                    if (!existingSelectValueSectionIds.Contains(selectValueSectionId))
                    {
                        _arpaContext.Add(new MusicianProfilePositionInner(selectValueSectionId, musicianProfileId));
                    }
                }
            }
        }
    }
}
