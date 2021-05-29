using System;
using System.Text.Json.Serialization;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MusicianProfileApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class ProjectParticipationDto : BaseEntityDto
    {
        public Guid? ParticipationStatusInnerId { get; set; }

        public string ParticipationStatusInner { get; set; }

        public Guid? ParticipationStatusInternalId { get; set; }

        public string ParticipationStatusInternal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public Guid? InvitationStatusId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public string InvitationStatus { get; set; }

        public string CommentByPerformerInner { get; set; }

        public string CommentByStaffInner { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public string CommentTeam { get; set; }

        public ReducedMusicianProfileDto MusicianProfile { get; set; }

        public ReducedProjectDto Project { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public ReducedPersonDto Person { get; set; }
    }

    public class ProjectParticipationDtoMappingProfile : Profile
    {
        public ProjectParticipationDtoMappingProfile()
        {
            CreateMap<ProjectParticipation, ProjectParticipationDto>()
                .ForMember(dest => dest.CommentTeam, opt => opt.MapFrom(src => src.CommentTeam))
                .ForMember(dest => dest.CommentByStaffInner, opt => opt.MapFrom(src => src.CommentByStaffInner))
                .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.CommentByPerformerInner))
                .ForMember(dest => dest.InvitationStatus, opt => opt.MapFrom(src => src.InvitationStatus))
                .ForMember(dest => dest.InvitationStatusId, opt => opt.MapFrom(src => src.InvitationStatusId))
                .ForMember(dest => dest.ParticipationStatusInternal, opt => opt.MapFrom(src => src.ParticipationStatusInternal != null ? src.ParticipationStatusInternal.SelectValue.Name : null))
                .ForMember(dest => dest.ParticipationStatusInternalId, opt => opt.MapFrom(src => src.ParticipationStatusInternalId))
                .ForMember(dest => dest.ParticipationStatusInnerId, opt => opt.MapFrom(src => src.ParticipationStatusInnerId))
                .ForMember(dest => dest.ParticipationStatusInner, opt => opt.MapFrom(src => src.ParticipationStatusInner != null ? src.ParticipationStatusInner.SelectValue.Name : null))
                .ForMember(dest => dest.InvitationStatusId, opt => opt.MapFrom(src => src.InvitationStatusId))
                .ForMember(dest => dest.InvitationStatus, opt => opt.MapFrom(src => src.InvitationStatus != null ? src.InvitationStatus.SelectValue.Name : null))
                .ForMember(dest => dest.MusicianProfile, opt => opt.MapFrom(src => src.MusicianProfile))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.MusicianProfile.Person))
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .AfterMap<RoleBasedSetNullAction<ProjectParticipation, ProjectParticipationDto>>();
        }
    }
}
