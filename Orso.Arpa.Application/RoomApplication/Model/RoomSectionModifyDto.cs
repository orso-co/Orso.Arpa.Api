using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomSectionModifyDto : IdFromRouteDto<RoomSectionModifyBodyDto>
    {
    }

    public class RoomSectionModifyBodyDto
    {
        public int? Quantity { get; set; }
        public string Description { get; set; }
    }

    public class RoomSectionModifyDtoMappingProfile : Profile
    {
        public RoomSectionModifyDtoMappingProfile()
        {
            CreateMap<RoomSectionModifyDto, ModifyRoomSection.Command>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Body.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description));
        }
    }

    public class RoomSectionModifyDtoValidator : IdFromRouteDtoValidator<RoomSectionModifyDto, RoomSectionModifyBodyDto>
    {
        public RoomSectionModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new RoomSectionModifyBodyDtoValidator());
        }
    }

    public class RoomSectionModifyBodyDtoValidator : AbstractValidator<RoomSectionModifyBodyDto>
    {
        public RoomSectionModifyBodyDtoValidator()
        {
            RuleFor(c => c.Quantity)
                .GreaterThanOrEqualTo(0)
                .When(c => c.Quantity.HasValue);
            RuleFor(c => c.Description)
                .RestrictedFreeText(500);
        }
    }
}