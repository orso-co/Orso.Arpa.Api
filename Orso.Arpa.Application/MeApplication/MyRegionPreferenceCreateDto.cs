using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyRegionPreferenceCreateDto : IdFromRouteDto<MyRegionPreferenceCreateBodyDto> { }

    public class MyRegionPreferenceCreateBodyDto
    {
        public byte Rating { get; set; }
        public Guid RegionId { get; set; }
        public string Comment { get; set; }
        public RegionPreferenceType Type { get; set; }
    }

    public class MyRegionPreferenceCreateDtoMappingProfile : Profile
    {
        public MyRegionPreferenceCreateDtoMappingProfile()
        {
            CreateMap<MyRegionPreferenceCreateDto, CreateRegionPreference.Command>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Body.Rating))
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Body.Type))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Body.RegionId));

        }
    }

    public class MyRegionPreferenceCreateDtoValidator : IdFromRouteDtoValidator<MyRegionPreferenceCreateDto, MyRegionPreferenceCreateBodyDto>
    {
        public MyRegionPreferenceCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MyRegionPreferenceCreateBodyDtoValidator());
        }
    }

    public class MyRegionPreferenceCreateBodyDtoValidator : AbstractValidator<MyRegionPreferenceCreateBodyDto>
    {
        public MyRegionPreferenceCreateBodyDtoValidator()
        {
            RuleFor(dto => dto.Rating)
                .FiveStarRating();

            RuleFor(dto => dto.Comment)
                .RestrictedFreeText(500);

            RuleFor(dto => dto.RegionId)
                .NotEmpty();

            RuleFor(dto => dto.Type)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
