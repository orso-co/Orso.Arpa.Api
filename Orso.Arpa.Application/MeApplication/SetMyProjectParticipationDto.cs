using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.MeApplication
{
    public class SetMyProjectParticipationDto : IdFromRouteDto<SetMyProjectParticipationBodyDto>
    {
        [FromRoute]
        public Guid ProjectId { get; set; }
    }

    public class SetMyProjectParticipationBodyDto
    {
        public Guid StatusId { get; set; }
        public string Comment { get; set; }
    }

    public class SetMyProjectParticipationDtoValidator : IdFromRouteDtoValidator<SetMyProjectParticipationDto, SetMyProjectParticipationBodyDto>
    {
        public SetMyProjectParticipationDtoValidator()
        {
            RuleFor(d => d.ProjectId)
                .NotEmpty();

            RuleFor(d => d.Body)
                .SetValidator(new SetMyProjectParticipationBodyDtoValidator());
        }
    }

    public class SetMyProjectParticipationBodyDtoValidator : AbstractValidator<SetMyProjectParticipationBodyDto>
    {
        public SetMyProjectParticipationBodyDtoValidator()
        {
            RuleFor(d => d.StatusId)
                .NotEmpty();

            RuleFor(d => d.Comment)
                .GeneralText(500);
        }
    }

    public class SetMyProjectParticipationDtoMappingProfile : Profile
    {
        public SetMyProjectParticipationDtoMappingProfile()
        {
            CreateMap<SetMyProjectParticipationDto, SetProjectParticipation.Command>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment))
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Body.StatusId));
        }
    }
}
