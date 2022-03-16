using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.Venues;

namespace Orso.Arpa.Application.VenueApplication
{
    public class VenueModifyDto : IdFromRouteDto<VenueModifyBodyDto>
    {
    }

    public class VenueModifyBodyDto
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

    public class VenueModifyDtoMappingProfile : Profile
    {
        public VenueModifyDtoMappingProfile()
        {
            CreateMap<VenueModifyDto, Modify.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Body.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Body.Address2))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Body.Zip))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Body.City))
                .ForMember(dest => dest.UrbanDistrict, opt => opt.MapFrom(src => src.Body.UrbanDistrict))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Body.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Body.Country))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.AddressCommentInner));
        }
    }

    public class VenueModifyDtoValidator : IdFromRouteDtoValidator<VenueModifyDto, VenueModifyBodyDto>
    {
        public VenueModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new VenueModifyBodyDtoValidator());
        }
    }

    public class VenueModifyBodyDtoValidator : AbstractValidator<VenueModifyBodyDto>
    {
        public VenueModifyBodyDtoValidator()
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
