using System;
using AutoMapper;
using FluentValidation;

namespace Orso.Arpa.Application.Logic.Appointments
{
    public static class AddProject
    {
        public class Dto
        {
            public Guid Id { get; set; }
            public Guid ProjectId { get; set; }
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
                RuleFor(d => d.ProjectId)
                    .NotEmpty();
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Domain.Logic.Appointments.AddProject.Command>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
            }
        }
    }
}
