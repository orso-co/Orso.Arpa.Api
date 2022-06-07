using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetPrediction;

namespace Orso.Arpa.Application.AppointmentParticipationApplication
{
    public class AppointmentParticipationSetPredictionDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid PredictionId { get; set; }
    }

    public class AppointmentParticipationSetPredictionDtoValidator : AbstractValidator<AppointmentParticipationSetPredictionDto>
    {
        public AppointmentParticipationSetPredictionDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.PersonId)
                .NotEmpty();
            RuleFor(d => d.PredictionId)
                .NotEmpty();
        }
    }

    public class AppointmentParticipationSetPredictionDtoMappingProfile : Profile
    {
        public AppointmentParticipationSetPredictionDtoMappingProfile()
        {
            CreateMap<AppointmentParticipationSetPredictionDto, Command>();
        }
    }
}
