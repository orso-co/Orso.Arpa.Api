using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;
using static Orso.Arpa.Domain.Logic.Appointments.SetDates;

namespace Orso.Arpa.Application.Logic.Appointments
{
    public static class SetDates
    {
        public class Dto : IModifyDto
        {
            public Guid Id { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Command>();
            }
        }

        public class Validator : AbstractValidator<Dto>
        {
            public Validator()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(d => d)
                    .NotNull();
                RuleFor(c => c.EndTime)
                        .NotNull()
                        .When(c => c.StartTime == null);
                RuleFor(c => c.StartTime)
                    .NotNull()
                    .When(c => c.EndTime == null);
                RuleFor(d => d.Id)
                    .NotEmpty();
            }
        }
    }
}
