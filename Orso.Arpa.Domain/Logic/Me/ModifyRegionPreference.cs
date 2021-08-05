using System;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Me
{
    public static class ModifyRegionPreference
    {
        public class Command : IModifyCommand<RegionPreference>
        {
            public Guid Id { get; set; }
            public Guid MusicianProfileId { get; set; }

            public byte Rating { get; set; }
            public string Comment { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, RegionPreference>()
                    .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                    .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .MustAsync(async (dto, regionPreferenceId, cancellation) => await arpaContext
                        .Set<RegionPreference>()
                        .AnyAsync(ar => ar.Id == regionPreferenceId && ar.MusicianProfileId == dto.Id, cancellation))
                    .WithErrorCode("404")
                    .WithMessage("Region preference could not be found.");
            }
        }
    }
}
