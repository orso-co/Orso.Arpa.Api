using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.AppointmentParticipationApplication.Model
{
    public class AppointmentParticipationSetPredictionDto : IdFromRouteDto<AppointmentParticipationSetPredictionBodyDto>
    {
        [FromRoute]
        public Guid PersonId { get; set; }
    }

    public class AppointmentParticipationSetPredictionBodyDto
    {
        public AppointmentParticipationPrediction Prediction { get; set; }
        public string CommentByPerformerInner { get; set; }
    }

    public class AppointmentParticipationSetPredictionDtoValidator : IdFromRouteDtoValidator<AppointmentParticipationSetPredictionDto, AppointmentParticipationSetPredictionBodyDto>
    {
        public AppointmentParticipationSetPredictionDtoValidator()
        {
            _ = RuleFor(d => d.PersonId)
                .NotEmpty();
            _ = RuleFor(d => d.Body)
                .SetValidator(new AppointmentParticipationSetPredictionBodyDtoValidator());
        }
    }

    public class AppointmentParticipationSetPredictionBodyDtoValidator : AbstractValidator<AppointmentParticipationSetPredictionBodyDto>
    {
        public AppointmentParticipationSetPredictionBodyDtoValidator()
        {
            _ = RuleFor(d => d.CommentByPerformerInner)
                .RestrictedFreeText(500);
            _ = RuleFor(d => d.Prediction)
                .IsInEnum();
        }
    }

    public class AppointmentParticipationSetPredictionDtoMappingProfile : Profile
    {
        public AppointmentParticipationSetPredictionDtoMappingProfile()
        {
            _ = CreateMap<AppointmentParticipationSetPredictionDto, SetAppointmentParticipationPrediction.Command>()
                .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.Body.CommentByPerformerInner))
                .ForMember(dest => dest.Prediction, opt => opt.MapFrom(src => src.Body.Prediction));

        }
    }
}
