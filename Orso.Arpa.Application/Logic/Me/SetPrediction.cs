using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetPrediction;

namespace Orso.Arpa.Application.Logic.Me
{
    public static class SetPrediction
    {
        public class Dto
        {
            public Guid Id { get; set; }
            public Guid PredictionId { get; set; }
        }

        public class Validator : AbstractValidator<Dto>
        {
            public Validator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(d => d)
                    .NotNull();
                RuleFor(d => d.Id)
                    .NotEmpty();
                RuleFor(d => d.PredictionId)
                    .NotEmpty();
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Command>();
            }
        }
    }
}
