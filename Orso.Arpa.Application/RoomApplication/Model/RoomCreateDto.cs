using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Enums;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomCreateDto : IdFromRouteDto<RoomCreateBodyDto>
    {
    }
    public class RoomCreateBodyDto
    {
        public string Name { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public CeilingHeight? CeilingHeight { get; set; }
        public Guid? CapacityId { get; set; }
    }

    public class RoomCreateDtoMappingProfile : Profile
    {
        public RoomCreateDtoMappingProfile()
        {
            CreateMap<RoomCreateDto, CreateRoom.Command>()
                .ForMember(dest => dest.VenueId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Body.Building))
                .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Body.Floor))
                .ForMember(dest => dest.CeilingHeight, opt => opt.MapFrom(src => src.Body.CeilingHeight))
                .ForMember(dest => dest.CapacityId, opt => opt.MapFrom(src => src.Body.CapacityId));
        }
    }

    public class RoomCreateDtoValidator : IdFromRouteDtoValidator<RoomCreateDto, RoomCreateBodyDto>
    {
        public RoomCreateDtoValidator()
        {
            _ = RuleFor(d => d.Body)
           .SetValidator(new RoomCreateBodyDtoValidator());
        }
    }

    public class RoomCreateBodyDtoValidator : AbstractValidator<RoomCreateBodyDto>
    {
        public RoomCreateBodyDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .PlaceName(50);
            RuleFor(c => c.Building)
                .PlaceName(50);
            RuleFor(c => c.Floor)
                .PlaceName(50);
            RuleFor(c => c.CeilingHeight)
                .IsInEnum()
                .When(c => c.CeilingHeight.HasValue);
        }
    }
}