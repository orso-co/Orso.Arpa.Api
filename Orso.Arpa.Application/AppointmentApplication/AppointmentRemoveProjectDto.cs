using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.RemoveProject;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentRemoveProjectDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class AppointmentRemoveProjectDtoMappingProfile : Profile
    {
        public AppointmentRemoveProjectDtoMappingProfile()
        {
            CreateMap<AppointmentRemoveProjectDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
        }
    }

    public class AppointmentRemoveProjectDtoValidator : AbstractValidator<AppointmentRemoveProjectDto>
    {
        public AppointmentRemoveProjectDtoValidator()
        {
            
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.ProjectId)
                .NotEmpty();
        }
    }
}
