using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.AddSection;

namespace Orso.Arpa.Application.Logic.Appointments
{
    public static class AddSection
    {
        public class Dto
        {
            public Guid Id { get; set; }

            public Guid SectionId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Command>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.SectionId));
            }
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
                RuleFor(d => d.SectionId)
                    .NotEmpty();
            }
        }
    }
}
