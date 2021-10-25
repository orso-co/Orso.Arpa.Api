using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.Addresses;

namespace Orso.Arpa.Application.AddressApplication
{
    public class AddressCreateDto : IdFromRouteDto<AddressCreateBodyDto>
    {
    }

    public class AddressCreateBodyDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string UrbanDistrict { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string CommentInner { get; set; }
        public Guid? TypeId { get; set; }
    }

    public class AddressCreateDtoMappingProfile : Profile
    {
        public AddressCreateDtoMappingProfile()
        {
            CreateMap<AddressCreateDto, Create.Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Body.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Body.Address2))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Body.Zip))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Body.City))
                .ForMember(dest => dest.UrbanDistrict, opt => opt.MapFrom(src => src.Body.UrbanDistrict))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Body.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Body.Country))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.CommentInner))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId));
        }
    }

    public class AddressCreateDtoValidator : IdFromRouteDtoValidator<AddressCreateDto, AddressCreateBodyDto>
    {
        public AddressCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new AddressCreateBodyDtoValidator());
        }
    }
    public class AddressCreateBodyDtoValidator : AbstractValidator<AddressCreateBodyDto>
    {
        public AddressCreateBodyDtoValidator()
        {
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

            RuleFor(c => c.CommentInner)
                .RestrictedFreeText(500);
        }
    }
}
