using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentAddProjectDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class AppointmentAddProjectDtoValidator : AbstractValidator<AppointmentAddProjectDto>
    {
        public AppointmentAddProjectDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.ProjectId)
                .NotEmpty();
        }
    }

    public class AppointmentAddProjectDtoMappingProfile : Profile
    {
        public AppointmentAddProjectDtoMappingProfile()
        {
            CreateMap<AppointmentAddProjectDto, AddProject.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
        }
    }
}
