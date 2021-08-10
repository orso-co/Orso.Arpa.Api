using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyRegionPreferenceRemoveDto
    {
        public Guid Id { get; set; }
        public Guid RegionPreferenceId { get; set; }
    }

    public class MyRegionPreferenceRemoveDtoMappingProfile : Profile
    {
        public MyRegionPreferenceRemoveDtoMappingProfile()
        {
            CreateMap<MyRegionPreferenceRemoveDto, RemoveRegionPreference.Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id));
        }
    }

    public class MyRegionPreferenceRemoveDtoValidator : AbstractValidator<MyRegionPreferenceRemoveDto>
    {
        public MyRegionPreferenceRemoveDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty();

            RuleFor(dto => dto.RegionPreferenceId)
                .NotEmpty();
        }
    }
}
