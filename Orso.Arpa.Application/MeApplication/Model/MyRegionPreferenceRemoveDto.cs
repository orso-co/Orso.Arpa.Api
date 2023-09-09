using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.RegionDomain.Commands;

namespace Orso.Arpa.Application.MeApplication.Model
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
            CreateMap<MyRegionPreferenceRemoveDto, RemoveMyRegionPreference.Command>()
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
