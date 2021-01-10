using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetPrediction;

namespace Orso.Arpa.Application.MeApplication
{
    public class SetMyProjectAppointmentPredictionDto
    {
        public Guid Id { get; set; }
        public Guid PredictionId { get; set; }
    }

    public class SetMyProjectAppointmentPredictionDtoValidator : AbstractValidator<SetMyProjectAppointmentPredictionDto>
    {
        public SetMyProjectAppointmentPredictionDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.PredictionId)
                .NotEmpty();
        }
    }

    public class SetMyProjectAppointmentPredictionDtoMappingProfile : Profile
    {
        public SetMyProjectAppointmentPredictionDtoMappingProfile()
        {
            CreateMap<SetMyProjectAppointmentPredictionDto, Command>();
        }
    }
}
