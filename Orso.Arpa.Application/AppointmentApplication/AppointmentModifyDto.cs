using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;
using static Orso.Arpa.Domain.Logic.Appointments.Modify;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentModifyDto : IModifyDto
    {
        public Guid Id { get; set; }

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

    public class AppointmentModifyDtoMappingProfile : Profile
    {
        public AppointmentModifyDtoMappingProfile()
        {
            CreateMap<AppointmentModifyDto, Command>();
        }
    }

    public class AppointmentModifyDtoValidator : AbstractValidator<AppointmentModifyDto>
    {
        public AppointmentModifyDtoValidator()
        {

            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.StartTime)
                .NotEmpty();
            RuleFor(d => d.EndTime)
                .NotEmpty()
                .Must((dto, endTime) => endTime >= dto.StartTime)
                .WithMessage("EndTime must be greater than StartTime");
            RuleFor(d => d.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(d => d.InternalDetails)
                .MaximumLength(1000);
            RuleFor(d => d.PublicDetails)
                .MaximumLength(1000);
        }
    }
}
