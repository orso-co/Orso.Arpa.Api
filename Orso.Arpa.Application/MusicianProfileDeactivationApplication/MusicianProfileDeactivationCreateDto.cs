using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.MusicianProfileDeactivations;

namespace Orso.Arpa.Application.MusicianProfileDeactivationApplication
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
            CreateMap<MusicianProfileDeactivationCreateDto, Create.Command>()
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
                .GeneralText(500);

            RuleFor(dto => dto.DeactivationStart)
                .NotEmpty();
        }
    }
}
