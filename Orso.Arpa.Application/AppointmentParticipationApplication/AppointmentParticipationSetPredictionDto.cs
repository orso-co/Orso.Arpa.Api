using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetPrediction;

namespace Orso.Arpa.Application.AppointmentParticipationApplication
{
    public class AppointmentParticipationSetPredictionDto : IdFromRouteDto<AppointmentParticipationSetPredictionBodyDto>
    {
        [FromRoute]
        public Guid PersonId { get; set; }
        [FromRoute]
        public Guid PredictionId { get; set; }
    }

    public class AppointmentParticipationSetPredictionBodyDto
    {
        public string CommentByPerformerInner { get; set; }
    }

    public class AppointmentParticipationSetPredictionDtoValidator : IdFromRouteDtoValidator<AppointmentParticipationSetPredictionDto, AppointmentParticipationSetPredictionBodyDto>
    {
        public AppointmentParticipationSetPredictionDtoValidator()
        {
            RuleFor(d => d.PersonId)
                .NotEmpty();
            RuleFor(d => d.PredictionId)
                .NotEmpty();
            RuleFor(d => d.Body)
                .SetValidator(new AppointmentParticipationSetPredictionBodyDtoValidator());
        }
    }

    public class AppointmentParticipationSetPredictionBodyDtoValidator : AbstractValidator<AppointmentParticipationSetPredictionBodyDto>
    {
        public AppointmentParticipationSetPredictionBodyDtoValidator()
        {
            RuleFor(d => d.CommentByPerformerInner)
                .RestrictedFreeText(500);
        }
    }

    public class AppointmentParticipationSetPredictionDtoMappingProfile : Profile
    {
        public AppointmentParticipationSetPredictionDtoMappingProfile()
        {
            CreateMap<AppointmentParticipationSetPredictionDto, Command>()
                .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.Body.CommentByPerformerInner));

        }
    }
}
