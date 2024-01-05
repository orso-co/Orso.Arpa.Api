using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomSectionCreateDto : IdFromRouteDto<RoomSectionCreateBodyDto>
    {
    }

    public class RoomSectionCreateBodyDto
    {
        public Guid InstrumentId { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
    }

    public class RoomSectionCreateDtoMappingProfile : Profile
    {
        public RoomSectionCreateDtoMappingProfile()
        {
            CreateMap<RoomSectionCreateDto, CreateRoomSection.Command>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Body.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description));
        }
    }

    public class RoomSectionCreateDtoValidator : IdFromRouteDtoValidator<RoomSectionCreateDto, RoomSectionCreateBodyDto>
    {
        public RoomSectionCreateDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new RoomSectionCreateBodyDtoValidator());
        }
    }

    public class RoomSectionCreateBodyDtoValidator : AbstractValidator<RoomSectionCreateBodyDto>
    {
        public RoomSectionCreateBodyDtoValidator()
        {
            RuleFor(c => c.InstrumentId)
                .NotEmpty();
            RuleFor(c => c.Quantity)
                .GreaterThanOrEqualTo(0)
                .When(c => c.Quantity.HasValue);
            RuleFor(c => c.Description)
                .RestrictedFreeText(500);
        }
    }
}