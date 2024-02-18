using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class PersonAddressModifyDto : IdFromRouteDto<PersonAddressModifyBodyDto>
    {
        [FromRoute]
        public Guid AddressId { get; set; }
    }

    public class PersonAddressModifyBodyDto
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

    public class PersonAddressModifyDtoMappingProfile : Profile
    {
        public PersonAddressModifyDtoMappingProfile()
        {
            _ = CreateMap<PersonAddressModifyDto, ModifyAddress.Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Body.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Body.Address2))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Body.Zip))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Body.City))
                .ForMember(dest => dest.UrbanDistrict, opt => opt.MapFrom(src => src.Body.UrbanDistrict))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Body.Country))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.CommentInner))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Body.State))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId));
        }
    }

    public class PersonAddressModifyDtoValidator : IdFromRouteDtoValidator<PersonAddressModifyDto, PersonAddressModifyBodyDto>
    {
        public PersonAddressModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new PersonAddressModifyBodyDtoValidator());

            _ = RuleFor(dto => dto.AddressId)
                .NotEmpty();
        }
    }

    public class PersonAddressModifyBodyDtoValidator : AbstractValidator<PersonAddressModifyBodyDto>
    {
        public PersonAddressModifyBodyDtoValidator()
        {
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

            _ = RuleFor(c => c.CommentInner)
                .RestrictedFreeText(500);
        }
    }
}
