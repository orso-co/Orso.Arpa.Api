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

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public static class Modify
    {
        public class Command : IRequest<MusicianProfile>, IHasInstrumentRequest
        {
            public Guid Id { get; set; }
            public Guid InstrumentId { get; set; }

            public bool IsMainProfile { get; set; }
            public bool IsDeactivated { get; set; }

            public byte LevelAssessmentInner { get; set; }
            public byte LevelAssessmentTeam { get; set; }
            public byte ProfilePreferenceInner { get; set; }
            public byte ProfilePreferenceTeam { get; set; }

            public string BackgroundInner { get; set; }
            public string BackgroundTeam { get; set; }
            public string SalaryComment { get; set; }
            public Guid QualificationId { get; set; }

            public Guid? SalaryId { get; set; }

            public Guid? InquiryStatusInnerId { get; set; }

            public Guid? InquiryStatusTeamId { get; set; }

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
                RuleFor(c => c.ExistingMusicianProfile)
                    .NotEmpty();

                RuleFor(c => c.IsMainProfile)
                    .Must((command, isMainProfile) => isMainProfile || (!isMainProfile && !command.ExistingMusicianProfile.IsMainProfile))
                    .WithMessage("You may not turn off the IsMainProfile flag");

                RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                RuleFor(c => c.InquiryStatusInnerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusInner);

                RuleFor(c => c.InquiryStatusTeamId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusTeam);

                RuleFor(c => c.SalaryId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Salary);

                RuleForEach(c => c.PreferredPositionsInnerIds)
                    .MusicianProfilePosition(arpaContext, nameof(Command.PreferredPositionsInnerIds));

                RuleForEach(c => c.PreferredPositionsTeamIds)
                    .MusicianProfilePosition(arpaContext, nameof(Command.PreferredPositionsTeamIds));

                RuleForEach(c => c.PreferredPartsInner)
                    .InstrumentPart(arpaContext);

                RuleForEach(c => c.PreferredPartsTeam)
                    .InstrumentPart(arpaContext);

                RuleFor(c => c.IsDeactivated)
                    .Must((command, isDeactivated, context) => !isDeactivated || !command.ExistingMusicianProfile.ProjectParticipations.Select(pp => pp.Project).All(p => p.IsCompleted))
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
                    .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.LevelAssessmentTeam))
                    .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.ProfilePreferenceInner))
                    .ForMember(dest => dest.ProfilePreferenceTeam, opt => opt.MapFrom(src => src.ProfilePreferenceTeam))

                    .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.BackgroundInner))
                    .ForMember(dest => dest.BackgroundTeam, opt => opt.MapFrom(src => src.BackgroundTeam))
                    .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.SalaryComment))

                    .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId))
                    .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.SalaryId))
                    .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.InquiryStatusInnerId))
                    .ForMember(dest => dest.InquiryStatusTeamId, opt => opt.MapFrom(src => src.InquiryStatusTeamId))

                    .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.PreferredPartsInner))
                    .ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.PreferredPartsTeam))

                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Handler : IRequestHandler<Command, MusicianProfile>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(IArpaContext arpaContext, IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<MusicianProfile> Handle(Command request, CancellationToken cancellationToken)
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
                UpdatePreferredPositionsTeam(modifiedEntity.PreferredPositionsTeam, request.PreferredPositionsTeamIds, modifiedEntity.Id);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return await _arpaContext.MusicianProfiles.FindAsync(new object[] { request.Id }, cancellationToken);
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

            private void UpdatePreferredPositionsTeam(ICollection<MusicianProfilePositionTeam> collectionToUpdate, IList<Guid> updateList, Guid musicianProfileId)
            {
                if (updateList.Count == 0)
                {
                    collectionToUpdate.Clear();
                    return;
                }

                foreach (MusicianProfilePositionTeam position in collectionToUpdate)
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
                        _arpaContext.Add(new MusicianProfilePositionTeam(selectValueSectionId, musicianProfileId));
                    }
                }
            }
        }
    }
}
