using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.MyContactDetails;

namespace Orso.Arpa.Application.MyContactDetailApplication
{
    public class MyContactDetailModifyDto : IdFromRouteDto<MyContactDetailModifyBodyDto>
    {
    }

    public class MyContactDetailModifyBodyDto
    {
        public ContactDetailKey Key { get; set; }
        public string Value { get; set; }
        public Guid? TypeId { get; set; }
        public string CommentInner { get; set; }
        public byte Preference { get; set; }
    }

    public class ContactDetailModifyDtoMappingProfile : Profile
    {
        public ContactDetailModifyDtoMappingProfile()
        {
            _ = CreateMap<MyContactDetailModifyDto, Modify.Command>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Body.Key))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Body.Value))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.CommentInner))
                .ForMember(dest => dest.Preference, opt => opt.MapFrom(src => src.Body.Preference));
        }
    }

    public class MyContactDetailModifyDtoValidator : IdFromRouteDtoValidator<MyContactDetailModifyDto, MyContactDetailModifyBodyDto>
    {
        public MyContactDetailModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                 .SetValidator(new MyContactDetailModifyBodyDtoValidator());
        }
    }

    public class MyContactDetailModifyBodyDtoValidator : AbstractValidator<MyContactDetailModifyBodyDto>
    {
        public MyContactDetailModifyBodyDtoValidator()
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
