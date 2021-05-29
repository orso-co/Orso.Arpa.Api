using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Appointments.Modify;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentModifyDto : IdFromRouteDto<AppointmentModifyBodyDto>
    {
    }

    public class AppointmentModifyBodyDto
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

    public class AppointmentModifyDtoMappingProfile : Profile
    {
        public AppointmentModifyDtoMappingProfile()
        {
            CreateMap<AppointmentModifyDto, Command>()
                .ForMember(dest => dest.ExpectationId, opt => opt.MapFrom(src => src.Body.ExpectationId))
                .ForMember(dest => dest.SalaryPatternId, opt => opt.MapFrom(src => src.Body.SalaryPatternId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.Body.SalaryId))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Body.StatusId))
                .ForMember(dest => dest.InternalDetails, opt => opt.MapFrom(src => src.Body.InternalDetails))
                .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.Body.PublicDetails))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Body.EndTime))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Body.StartTime))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Body.CategoryId));
        }
    }

    public class AppointmentModifyDtoValidator : BaseModifyDtoValidator<AppointmentModifyDto, AppointmentModifyBodyDto>
    {
        public AppointmentModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new AppointmentModifyBodyDtoValidator());
        }
    }

    public class AppointmentModifyBodyDtoValidator : AbstractValidator<AppointmentModifyBodyDto>
    {
        public AppointmentModifyBodyDtoValidator()
        {
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
