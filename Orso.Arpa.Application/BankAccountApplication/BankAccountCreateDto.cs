using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.BankAccounts.Create;

namespace Orso.Arpa.Application.BankAccountApplication
{
    public class BankAccountCreateDto : IdFromRouteDto<BankAccountCreateBodyDto>
    {
    }

    public class BankAccountCreateBodyDto
    {
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string CommentInner { get; set; }
        public string AccountOwner { get; set; }
    }
    public class BankAccountCreateDtoMappingProfile : Profile
    {
        public BankAccountCreateDtoMappingProfile()
        {
            CreateMap<BankAccountCreateDto, Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Iban, opt => opt.MapFrom(src => src.Body.Iban))
                .ForMember(dest => dest.Bic, opt => opt.MapFrom(src => src.Body.Bic))
                .ForMember(dest => dest.AccountOwner, opt => opt.MapFrom(src => src.Body.AccountOwner))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.CommentInner));
        }
    }

    public class BankAccountCreateDtoValidator : IdFromRouteDtoValidator<BankAccountCreateDto, BankAccountCreateBodyDto>
    {
        public BankAccountCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new BankAccountCreateBodyDtoValidator());
        }
    }
    public class BankAccountCreateBodyDtoValidator : AbstractValidator<BankAccountCreateBodyDto>
    {
        public BankAccountCreateBodyDtoValidator()
        {
            RuleFor(c => c.Iban)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .Iban();

            RuleFor(c => c.Bic)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .When(dto => !dto.Iban.StartsWith("de", StringComparison.InvariantCultureIgnoreCase))
                .Bic()
                .When(dto => !string.IsNullOrWhiteSpace(dto.Bic));

            RuleFor(c => c.CommentInner)
                .GeneralText(500);

            RuleFor(c => c.AccountOwner)
                .GeneralText(50);
        }
    }
}
