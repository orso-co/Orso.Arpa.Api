using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.Create;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentCreateDto
    {
        public Guid? CategoryId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PublicDetails { get; set; }
        public string InternalDetails { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? SalaryId { get; set; }
        public Guid? SalaryPatternId { get; set; }
        public Guid? ExpectationId { get; set; }
    }

    public class AppointmentCreateDtoMappingProfile : Profile
    {
        public AppointmentCreateDtoMappingProfile()
        {
            CreateMap<AppointmentCreateDto, Command>();
        }
    }

    public class AppointmentCreateDtoValidator : AbstractValidator<AppointmentCreateDto>
    {
        public AppointmentCreateDtoValidator()
        {

            RuleFor(d => d)
                .NotNull();
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
