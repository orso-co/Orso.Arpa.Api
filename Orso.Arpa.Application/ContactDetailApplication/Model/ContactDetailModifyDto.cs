using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.ContactDetailApplication.Model
{
    public class ContactDetailModifyDto : IdFromRouteDto<ContactDetailModifyBodyDto>
    {
        [FromRoute]
        public Guid ContactDetailId { get; set; }
    }

    public class ContactDetailModifyBodyDto
    {
        public ContactDetailKey Key { get; set; }
        public string Value { get; set; }
        public Guid? TypeId { get; set; }
        public string CommentTeam { get; set; }
        public byte Preference { get; set; }
    }

    public class ContactDetailModifyDtoMappingProfile : Profile
    {
        public ContactDetailModifyDtoMappingProfile()
        {
            _ = CreateMap<ContactDetailModifyDto, ModifyContactDetails.Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ContactDetailId))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Body.Key))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Body.Value))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.CommentTeam, opt => opt.MapFrom(src => src.Body.CommentTeam))
                .ForMember(dest => dest.Preference, opt => opt.MapFrom(src => src.Body.Preference));
        }
    }

    public class ContactDetailModifyDtoValidator : IdFromRouteDtoValidator<ContactDetailModifyDto, ContactDetailModifyBodyDto>
    {
        public ContactDetailModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new ContactDetailModifyBodyDtoValidator());

            _ = RuleFor(dto => dto.ContactDetailId)
                .NotEmpty();
        }
    }

    public class ContactDetailModifyBodyDtoValidator : AbstractValidator<ContactDetailModifyBodyDto>
    {
        public ContactDetailModifyBodyDtoValidator()
        {
            _ = RuleFor(c => c.Key)
                 .NotEmpty()
                 .IsInEnum();

            _ = RuleFor(c => c.Value)
                .NotEmpty();

            _ = RuleFor(c => c.Value)
                .ValidUri(1000)
                .When(dto => ContactDetailKey.Url.Equals(dto?.Key));

            _ = RuleFor(c => c.Value)
                .EmailAddress()
                .When(dto => ContactDetailKey.EMail.Equals(dto?.Key));

            _ = RuleFor(c => c.Value)
                .PhoneNumber()
                .When(dto => ContactDetailKey.PhoneNumber.Equals(dto?.Key));

            _ = RuleFor(c => c.CommentTeam)
                .RestrictedFreeText(500);

            _ = RuleFor(c => c.Preference)
                .FiveStarRating();
        }
    }
}
