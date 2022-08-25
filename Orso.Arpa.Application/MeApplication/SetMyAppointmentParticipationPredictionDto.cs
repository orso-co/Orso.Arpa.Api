using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetPrediction;

namespace Orso.Arpa.Application.MeApplication
{
    public class SetMyAppointmentParticipationPredictionDto : IdFromRouteDto<SetMyAppointmentParticipationPredictionBodyDto>
    {
        [FromRoute]
        public Guid PredictionId { get; set; }
    }

    public class SetMyAppointmentParticipationPredictionBodyDto
    {
        public string CommentByPerformerInner { get; set; }
    }

    public class SetMyProjectAppointmentPredictionDtoValidator : IdFromRouteDtoValidator<SetMyAppointmentParticipationPredictionDto, SetMyAppointmentParticipationPredictionBodyDto>
    {
        public SetMyProjectAppointmentPredictionDtoValidator()
        {
            RuleFor(d => d.PredictionId)
                .NotEmpty();
            RuleFor(d => d.Body)
                .SetValidator(new SetMyAppointmentParticipationPredictionBodyDtoValidator());
        }
    }

    public class SetMyAppointmentParticipationPredictionBodyDtoValidator : AbstractValidator<SetMyAppointmentParticipationPredictionBodyDto>
    {
        public SetMyAppointmentParticipationPredictionBodyDtoValidator()
        {
            RuleFor(d => d.CommentByPerformerInner)
                .RestrictedFreeText(500);
        }
    }

    public class SetMyProjectAppointmentPredictionDtoMappingProfile : Profile
    {
        public SetMyProjectAppointmentPredictionDtoMappingProfile()
        {
            CreateMap<SetMyAppointmentParticipationPredictionDto, Command>()
                .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.Body.CommentByPerformerInner));
        }
    }
}
