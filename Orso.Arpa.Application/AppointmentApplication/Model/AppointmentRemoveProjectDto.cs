using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.AppointmentApplication.Model
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
            CreateMap<AppointmentRemoveProjectDto, RemoveProject.Command>()
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
