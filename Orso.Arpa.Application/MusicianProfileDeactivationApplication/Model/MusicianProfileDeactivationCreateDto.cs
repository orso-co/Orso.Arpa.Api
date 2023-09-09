using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;

namespace Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model
{
    public class MusicianProfileDeactivationCreateDto : IdFromRouteDto<MusicianProfileDeactivationCreateBodyDto>
    {
    }

    public class MusicianProfileDeactivationCreateBodyDto
    {
        public DateTime DeactivationStart { get; set; }
        public string Purpose { get; set; }
    }

    public class MusicianProfileDeactivationCreateDtoMappingProfile : Profile
    {
        public MusicianProfileDeactivationCreateDtoMappingProfile()
        {
            CreateMap<MusicianProfileDeactivationCreateDto, CreateMusicianProfileDeactivation.Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeactivationStart, opt => opt.MapFrom(src => src.Body.DeactivationStart))
                .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Body.Purpose));
        }
    }

    public class MusicianProfileDeactivationCreateDtoValidator : IdFromRouteDtoValidator<MusicianProfileDeactivationCreateDto, MusicianProfileDeactivationCreateBodyDto>
    {
        public MusicianProfileDeactivationCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MusicianProfileDeactivationCreateBodyDtoValidator());
        }
    }

    public class MusicianProfileDeactivationCreateBodyDtoValidator : AbstractValidator<MusicianProfileDeactivationCreateBodyDto>
    {
        public MusicianProfileDeactivationCreateBodyDtoValidator()
        {
            RuleFor(dto => dto.Purpose)
                .RestrictedFreeText(500);

            RuleFor(dto => dto.DeactivationStart)
                .NotEmpty();
        }
    }
}
