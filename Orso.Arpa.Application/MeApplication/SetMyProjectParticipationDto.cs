using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.ProjectParticipations;

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

    public class MyProjectParticipationDtoValidator : BaseModifyDtoValidator<SetMyProjectParticipationDto, SetMyProjectParticipationBodyDto>
    {
        public MyProjectParticipationDtoValidator()
        {
            RuleFor(d => d.ProjectId)
                .NotEmpty();

            RuleFor(d => d.Body)
                .SetValidator(new MyProjectParticipationBodyDtoValidator());
        }
    }

    public class MyProjectParticipationBodyDtoValidator : AbstractValidator<SetMyProjectParticipationBodyDto>
    {
        public MyProjectParticipationBodyDtoValidator()
        {
            RuleFor(d => d.StatusId)
                .NotEmpty();

            RuleFor(d => d.Comment)
                .MaximumLength(500);
        }
    }

    public class MyProjectParticipationDtoMappingProfile : Profile
    {
        public MyProjectParticipationDtoMappingProfile()
        {
            CreateMap<SetMyProjectParticipationDto, Set.Command>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment))
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Body.StatusId));
        }
    }
}
