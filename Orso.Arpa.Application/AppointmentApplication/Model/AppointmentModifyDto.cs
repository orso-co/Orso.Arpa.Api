using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.AppointmentApplication.Model
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
        public AppointmentStatus? Status { get; set; }
        public Guid? SalaryId { get; set; }
        public Guid? SalaryPatternId { get; set; }
        public Guid? ExpectationId { get; set; }
    }

    public class AppointmentModifyDtoMappingProfile : Profile
    {
        public AppointmentModifyDtoMappingProfile()
        {
            _ = CreateMap<AppointmentModifyDto, ModifyAppointment.Command>()
                .ForMember(dest => dest.ExpectationId, opt => opt.MapFrom(src => src.Body.ExpectationId))
                .ForMember(dest => dest.SalaryPatternId, opt => opt.MapFrom(src => src.Body.SalaryPatternId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.Body.SalaryId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Body.Status))
                .ForMember(dest => dest.InternalDetails, opt => opt.MapFrom(src => src.Body.InternalDetails))
                .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.Body.PublicDetails))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Body.EndTime))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Body.StartTime))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Body.CategoryId));
        }
    }

    public class AppointmentModifyDtoValidator : IdFromRouteDtoValidator<AppointmentModifyDto, AppointmentModifyBodyDto>
    {
        public AppointmentModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new AppointmentModifyBodyDtoValidator());
        }
    }

    public class AppointmentModifyBodyDtoValidator : AbstractValidator<AppointmentModifyBodyDto>
    {
        public AppointmentModifyBodyDtoValidator()
        {
            _ = RuleFor(d => d.StartTime)
               .NotEmpty();
            _ = RuleFor(d => d.EndTime)
                .NotEmpty()
                .Must((dto, endTime) => endTime >= dto.StartTime)
                .WithMessage("EndTime must be greater than StartTime");
            _ = RuleFor(d => d.Name)
                .NotEmpty()
                .FreeText(50);
            _ = RuleFor(d => d.InternalDetails)
                .RestrictedFreeText(1000);
            _ = RuleFor(d => d.PublicDetails)
                .RestrictedFreeText(1000);
            _ = RuleFor(d => d.Status)
                .IsInEnum();
        }
    }
}
