using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.CurriculumVitaeReferences
{
    public static class Modify
    {
        public class Command : IModifyCommand<CurriculumVitaeReference>
        {
            public Guid Id { get; set; }
            public string TimeSpan { get; set; }
            public string Institution { get; set; }
            public Guid TypeId { get; set; }
            public string Description { get; set; }
            public byte SortOrder { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Education>()
                    .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.TimeSpan))
                    .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Institution))
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.SortOrder))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Education>(arpaContext, nameof(Command.Id));
                RuleFor(c => c.TypeId)
                   .SelectValueMapping<Command, Education>(arpaContext, a => a.Type);
            }
        }
    }
}
