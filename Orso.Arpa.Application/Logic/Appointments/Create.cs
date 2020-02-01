using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.Create;

namespace Orso.Arpa.Application.Logic.Appointments
{
    public static class Create
    {
        public class Dto
        {
            public Guid? CategoryId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Name { get; set; }
            public string PublicDetails { get; set; }
            public string InternalDetails { get; set; }
            public Guid? StatusId { get; set; }
            public Guid? EmolumentId { get; set; }
            public Guid? EmolumentPatternId { get; set; }
            public Guid? ExpectationId { get; set; }
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
                RuleFor(d => d.CategoryId)
                    .NotEmpty();
                RuleFor(d => d.StatusId)
                    .NotEmpty();
                RuleFor(d => d.StartTime)
                    .NotEmpty();
                RuleFor(d => d.EndTime)
                    .NotEmpty()
                    .Must((dto, endTime) => endTime >= dto.StartTime)
                    .WithMessage("EndTime must be greater than StartTime");
                RuleFor(d => d.Name)
                    .NotEmpty();
                RuleFor(d => d.EmolumentId)
                    .NotEmpty();
            }
        }
    }
}
