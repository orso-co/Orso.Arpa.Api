using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Application.ProjectApplication
{
    public class SetProjectParticipationDto : IdFromRouteDto<SetProjectParticipationBodyDto>
    {
    }

    public class SetProjectParticipationBodyDto
    {
        public Guid? ParticipationStatusInnerId { get; set; }
        public Guid ParticipationStatusInternalId { get; set; }
        public Guid InvitationStatusId { get; set; }
        public string CommentByStaffInner { get; set; }
        public string CommentTeam { get; set; }
        public Guid MusicianProfileId { get; set; }

    }

    public class SetProjectParticipationDtoMappingProfile : Profile
    {
        public SetProjectParticipationDtoMappingProfile()
        {
            CreateMap<SetProjectParticipationDto, SetProjectParticipation.Command>()
                .ForMember(dest => dest.CommentByStaffInner, opt => opt.MapFrom(src => src.Body.CommentByStaffInner))
                .ForMember(dest => dest.CommentTeam, opt => opt.MapFrom(src => src.Body.CommentTeam))
                .ForMember(dest => dest.InvitationStatusId, opt => opt.MapFrom(src => src.Body.InvitationStatusId))
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Body.MusicianProfileId))
                .ForMember(dest => dest.ParticipationStatusInnerId, opt => opt.MapFrom(src => src.Body.ParticipationStatusInnerId))
                .ForMember(dest => dest.ParticipationStatusInternalId, opt => opt.MapFrom(src => src.Body.ParticipationStatusInternalId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id));
        }
    }

    public class SetProjectParticipationDtoValidator : IdFromRouteDtoValidator<SetProjectParticipationDto, SetProjectParticipationBodyDto>
    {
        public SetProjectParticipationDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new SetProjectParticipationBodyDtoValidator());
        }
    }

    public class SetProjectParticipationBodyDtoValidator : AbstractValidator<SetProjectParticipationBodyDto>
    {
        public SetProjectParticipationBodyDtoValidator()
        {
            RuleFor(d => d.MusicianProfileId)
                .NotEmpty();

            RuleFor(d => d.ParticipationStatusInternalId)
                .NotEmpty();

            RuleFor(d => d.InvitationStatusId)
                .NotEmpty();

            RuleFor(d => d.CommentByStaffInner)
                .MaximumLength(500);

            RuleFor(d => d.CommentTeam)
                .MaximumLength(500);
        }
    }
}
