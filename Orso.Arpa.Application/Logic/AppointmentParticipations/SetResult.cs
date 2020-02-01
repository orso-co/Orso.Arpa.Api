using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetResult;

namespace Orso.Arpa.Application.Logic.AppointmentParticipations
{
    public static class SetResult
    {
        public class Dto
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public Guid ResultId { get; set; }
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
                RuleFor(d => d.PersonId)
                    .NotEmpty();
                RuleFor(d => d.ResultId)
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
