using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class Modify
    {
        public class Command : IModifyCommand<Appointment>
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

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Appointment>()
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.PublicDetails, opt => opt.MapFrom(src => src.PublicDetails))
                    .ForMember(dest => dest.InternalDetails, opt => opt.MapFrom(src => src.InternalDetails))
                    .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                    .ForMember(dest => dest.EmolumentId, opt => opt.MapFrom(src => src.EmolumentId))
                    .ForMember(dest => dest.EmolumentPatternId, opt => opt.MapFrom(src => src.EmolumentPatternId))
                    .ForMember(dest => dest.ExpectationId, opt => opt.MapFrom(src => src.ExpectationId))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext);
            }
        }
    }
}
