using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Application.VenueApplication.Model
{
    public class VenueCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string UrbanDistrict { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string AddressCommentInner { get; set; }
    }

    public class VenueCreateDtoMappingProfile : Profile
    {
        public VenueCreateDtoMappingProfile()
        {
            _ = CreateMap<VenueCreateDto, CreateVenue.Command>()
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.AddressCommentInner));
        }
    }

    public class VenueCreateDtoValidator : AbstractValidator<VenueCreateDto>
    {
        public VenueCreateDtoValidator()
        {
            _ = RuleFor(v => v.Name)
                .NotEmpty()
                .RestrictedFreeText(50);

            _ = RuleFor(v => v.Description)
                .RestrictedFreeText(255);

            _ = RuleFor(c => c.Country)
                 .NotEmpty()
                 .PlaceName(100);

            _ = RuleFor(c => c.City)
                 .NotEmpty()
                 .PlaceName(100);

            _ = RuleFor(c => c.Zip)
                 .NotEmpty()
                 .PlaceName(20);

            _ = RuleFor(c => c.Address1)
                .PlaceName(100);

            _ = RuleFor(c => c.Address2)
                .PlaceName(100);

            _ = RuleFor(c => c.State)
                .PlaceName(100);

            _ = RuleFor(c => c.UrbanDistrict)
                .PlaceName(100);

            _ = RuleFor(c => c.AddressCommentInner)
                .RestrictedFreeText(500);
        }
    }
}
