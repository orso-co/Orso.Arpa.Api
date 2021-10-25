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
            CreateMap<MyContactDetailCreateDto, Create.Command>();
        }
    }

    public class MyContactDetailCreateDtoValidator : AbstractValidator<MyContactDetailCreateDto>
    {
        public MyContactDetailCreateDtoValidator()
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

            RuleFor(c => c.CommentInner)
                .RestrictedFreeText(500);

            RuleFor(c => c.Preference)
                .FiveStarRating();
        }
    }
}
