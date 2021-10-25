using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.ContactDetails;

namespace Orso.Arpa.Application.ContactDetailApplication
{
    public class ContactDetailCreateDto : IdFromRouteDto<ContactDetailCreateBodyDto>
    {
    }

    public class ContactDetailCreateBodyDto
    {
        public ContactDetailKey Key { get; set; }
        public string Value { get; set; }
        public Guid? TypeId { get; set; }
        public string CommentTeam { get; set; }
        public byte Preference { get; set; }
    }

    public class ContactDetailCreateDtoMappingProfile : Profile
    {
        public ContactDetailCreateDtoMappingProfile()
        {
            CreateMap<ContactDetailCreateDto, Create.Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Body.Key))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Body.Value))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.CommentTeam, opt => opt.MapFrom(src => src.Body.CommentTeam))
                .ForMember(dest => dest.Preference, opt => opt.MapFrom(src => src.Body.Preference));
        }
    }

    public class ContactDetailCreateDtoValidator : IdFromRouteDtoValidator<ContactDetailCreateDto, ContactDetailCreateBodyDto>
    {
        public ContactDetailCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new ContactDetailCreateBodyDtoValidator());
        }
    }
    public class ContactDetailCreateBodyDtoValidator : AbstractValidator<ContactDetailCreateBodyDto>
    {
        public ContactDetailCreateBodyDtoValidator()
        {
            RuleFor(c => c.Key)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .IsInEnum();

            RuleFor(c => c.Value)
                .NotEmpty();

            RuleFor(c => c.Value)
                .ValidUri(1000)
                .When(dto => ContactDetailKey.Url.Equals(dto?.Key));

            RuleFor(c => c.Value)
                .EmailAddress()
                .When(dto => ContactDetailKey.EMail.Equals(dto?.Key));

            RuleFor(c => c.Value)
                .PhoneNumber()
                .When(dto => ContactDetailKey.PhoneNumber.Equals(dto?.Key));

            RuleFor(c => c.CommentTeam)
                .RestrictedFreeText(500);

            RuleFor(c => c.Preference)
                .FiveStarRating();
        }
    }
}
