using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class SetProjectParticipationDto : IdFromRouteDto<SetProjectParticipationBodyDto>
    {
    }

    public class SetProjectParticipationBodyDto
    {
        public ProjectParticipationStatusInner? ParticipationStatusInner { get; set; }
        public ProjectParticipationStatusInternal ParticipationStatusInternal { get; set; }
        public ProjectInvitationStatus InvitationStatus { get; set; }
        public string CommentByStaffInner { get; set; }
        public string CommentTeam { get; set; }
        public Guid MusicianProfileId { get; set; }

    }

    public class SetProjectParticipationDtoMappingProfile : Profile
    {
        public SetProjectParticipationDtoMappingProfile()
        {
            _ = CreateMap<SetProjectParticipationDto, SetProjectParticipation.Command>()
                .ForMember(dest => dest.CommentByStaffInner, opt => opt.MapFrom(src => src.Body.CommentByStaffInner))
                .ForMember(dest => dest.CommentTeam, opt => opt.MapFrom(src => src.Body.CommentTeam))
                .ForMember(dest => dest.InvitationStatus, opt => opt.MapFrom(src => src.Body.InvitationStatus))
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Body.MusicianProfileId))
                .ForMember(dest => dest.ParticipationStatusInner, opt => opt.MapFrom(src => src.Body.ParticipationStatusInner))
                .ForMember(dest => dest.ParticipationStatusInternal, opt => opt.MapFrom(src => src.Body.ParticipationStatusInternal))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id));
        }
    }

    public class SetProjectParticipationDtoValidator : IdFromRouteDtoValidator<SetProjectParticipationDto, SetProjectParticipationBodyDto>
    {
        public SetProjectParticipationDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new SetProjectParticipationBodyDtoValidator());
        }
    }

    public class SetProjectParticipationBodyDtoValidator : AbstractValidator<SetProjectParticipationBodyDto>
    {
        public SetProjectParticipationBodyDtoValidator()
        {
            _ = RuleFor(d => d.MusicianProfileId)
                .NotEmpty();

            _ = RuleFor(d => d.ParticipationStatusInternal)
                .NotEmpty();

            _ = RuleFor(d => d.InvitationStatus)
                .NotEmpty();

            _ = RuleFor(d => d.CommentByStaffInner)
                .RestrictedFreeText(500);

            _ = RuleFor(d => d.CommentTeam)
                .RestrictedFreeText(500);
        }
    }
}
