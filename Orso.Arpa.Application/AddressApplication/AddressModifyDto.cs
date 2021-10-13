using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.Addresses;

namespace Orso.Arpa.Application.AddressApplication
{
    public class AddressModifyDto : IdFromRouteDto<AddressModifyBodyDto>
    {
        [FromRoute]
        public Guid AddressId { get; set; }
    }

    public class AddressModifyBodyDto
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

    public class AddressModifyDtoMappingProfile : Profile
    {
        public AddressModifyDtoMappingProfile()
        {
            CreateMap<AddressModifyDto, Modify.Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Body.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Body.Address2))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Body.Zip))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Body.City))
                .ForMember(dest => dest.UrbanDistrict, opt => opt.MapFrom(src => src.Body.UrbanDistrict))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Body.Country))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.CommentInner))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId));
        }
    }

    public class AddressModifyDtoValidator : IdFromRouteDtoValidator<AddressModifyDto, AddressModifyBodyDto>
    {
        public AddressModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new AddressModifyBodyDtoValidator());

            RuleFor(dto => dto.AddressId)
                .NotEmpty();
        }
    }

    public class AddressModifyBodyDtoValidator : AbstractValidator<AddressModifyBodyDto>
    {
        public AddressModifyBodyDtoValidator()
        {
            RuleFor(c => c.Country)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .GeneralText(100);

            RuleFor(c => c.City)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .GeneralText(100);

            RuleFor(c => c.Zip)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .GeneralText(20);

            RuleFor(c => c.Address1)
                .GeneralText(100);

            RuleFor(c => c.Address2)
                .GeneralText(100);

            RuleFor(c => c.State)
                .GeneralText(100);

            RuleFor(c => c.UrbanDistrict)
                .GeneralText(100);

            RuleFor(c => c.CommentInner)
                .GeneralText(500);
        }
    }
}
