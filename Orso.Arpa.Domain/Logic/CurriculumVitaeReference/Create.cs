using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.CurriculumVitaeReferences
{
    public static class Create
    {
        public class Command : ICreateCommand<CurriculumVitaeReference>
        {
            public Command(string timeSpan, string institution, Guid typeId,
                string description, byte sortOrder, Guid musicianProfileId)
            {
                TimeSpan = timeSpan;
                Institution = institution;
                TypeId = typeId;
                Description = description;
                SortOrder = sortOrder;
                MusicianProfileId = musicianProfileId;
            }

            public Command()
            {
            }

            public string TimeSpan { get; set; }
            public string Institution { get; set; }
            public Guid TypeId { get; set; }
            public string Description { get; set; }
            public byte SortOrder { get; set; }
            public Guid MusicianProfileId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Create.Command>()
                    .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.TimeSpan))
                    .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Institution))
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.SortOrder))
                    .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.MusicianProfileId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.MusicianProfileId)
                    .EntityExists<Command, MusicianProfile>(arpaContext, nameof(Command.MusicianProfileId));
                RuleFor(c => c.TypeId)
                    .SelectValueMapping<Command, Education>(arpaContext, a => a.Type);
            }
        }
    }
}
