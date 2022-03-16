using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Logic.Venues;

namespace Orso.Arpa.Application.VenueApplication
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
            CreateMap<VenueCreateDto, Create.Command>()
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.AddressCommentInner));
        }
    }

    public class VenueCreateDtoValidator : AbstractValidator<VenueCreateDto>
    {
        public VenueCreateDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .RestrictedFreeText(50);

            RuleFor(v => v.Description)
                .RestrictedFreeText(255);

            RuleFor(c => c.Country)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .PlaceName(100);

            RuleFor(c => c.City)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .PlaceName(100);

            RuleFor(c => c.Zip)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .PlaceName(20);

            RuleFor(c => c.Address1)
                .PlaceName(100);

            RuleFor(c => c.Address2)
                .PlaceName(100);

            RuleFor(c => c.State)
                .PlaceName(100);

            RuleFor(c => c.UrbanDistrict)
                .PlaceName(100);

            RuleFor(c => c.AddressCommentInner)
                .RestrictedFreeText(500);
        }
    }
}
