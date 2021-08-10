using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyRegionPreferenceModifyDto : IdFromRouteDto<MyRegionPreferenceModifyBodyDto>
    {
        [FromRoute]
        public Guid RegionPreferenceId { get; set; }
    }

    public class MyRegionPreferenceModifyBodyDto
    {
        public byte Rating { get; set; }

        public string Comment { get; set; }
    }

    public class MyRegionPreferenceModifyDtoMappingProfile : Profile
    {
        public MyRegionPreferenceModifyDtoMappingProfile()
        {
            CreateMap<MyRegionPreferenceModifyDto, ModifyRegionPreference.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RegionPreferenceId))
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Body.Rating))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment));
        }
    }

    public class MyRegionPreferenceModifyDtoValidator : IdFromRouteDtoValidator<MyRegionPreferenceModifyDto, MyRegionPreferenceModifyBodyDto>
    {
        public MyRegionPreferenceModifyDtoValidator()
        {
            RuleFor(d => d.RegionPreferenceId)
                .NotEmpty();

            RuleFor(d => d.Body)
                .SetValidator(new MyRegionPreferenceModifyBodyDtoValidator());
        }
    }

    public class MyRegionPreferenceModifyBodyDtoValidator : AbstractValidator<MyRegionPreferenceModifyBodyDto>
    {
        public MyRegionPreferenceModifyBodyDtoValidator()
        {
            RuleFor(dto => dto.Rating)
                .FiveStarRating();

            RuleFor(dto => dto.Comment)
                .GeneralText(500);
        }
    }
}
