using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Enums;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomModifyDto : IdFromRouteDto<RoomModifyBodyDto>
    {
    }
    public class RoomModifyBodyDto
    {
        public string Name { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public CeilingHeight? CeilingHeight { get; set; }
        public Guid? CapacityId { get; set; }
    }

    public class RoomModifyDtoMappingProfile : Profile
    {
        public RoomModifyDtoMappingProfile()
        {
            CreateMap<RoomModifyDto, ModifyRoom.Command>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Body.Building))
                .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Body.Floor))
                .ForMember(dest => dest.CeilingHeight, opt => opt.MapFrom(src => src.Body.CeilingHeight))
                .ForMember(dest => dest.CapacityId, opt => opt.MapFrom(src => src.Body.CapacityId));
        }
    }

    public class RoomModifyDtoValidator : IdFromRouteDtoValidator<RoomModifyDto, RoomModifyBodyDto>
    {
        public RoomModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
           .SetValidator(new RoomModifyBodyDtoValidator());
        }
    }

    public class RoomModifyBodyDtoValidator : AbstractValidator<RoomModifyBodyDto>
    {
        public RoomModifyBodyDtoValidator()
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