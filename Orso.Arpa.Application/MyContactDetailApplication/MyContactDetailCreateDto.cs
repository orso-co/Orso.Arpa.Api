using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.MyContactDetails;

namespace Orso.Arpa.Application.MyContactDetailApplication
{
    public class MyContactDetailCreateDto
    {
        public ContactDetailKey Key { get; set; }
        public string Value { get; set; }
        public Guid? TypeId { get; set; }
        public string CommentInner { get; set; }
        public byte Preference { get; set; }
    }

    public class MyContactDetailCreateDtoMappingProfile : Profile
    {
        public MyContactDetailCreateDtoMappingProfile()
        {
            _ = CreateMap<MyContactDetailCreateDto, Create.Command>();
        }
    }

    public class MyContactDetailCreateDtoValidator : AbstractValidator<MyContactDetailCreateDto>
    {
        public MyContactDetailCreateDtoValidator()
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

            _ = RuleFor(c => c.CommentInner)
                .RestrictedFreeText(500);

            _ = RuleFor(c => c.Preference)
                .FiveStarRating();
        }
    }
}
