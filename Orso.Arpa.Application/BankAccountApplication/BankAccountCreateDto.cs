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
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public string CommentInner { get; set; }
    }
    public class BankAccountCreateDtoMappingProfile : Profile
    {
        public BankAccountCreateDtoMappingProfile()
        {
            CreateMap<BankAccountCreateDto, Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IBAN, opt => opt.MapFrom(src => src.Body.IBAN))
                .ForMember(dest => dest.BIC, opt => opt.MapFrom(src => src.Body.BIC))
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
            RuleFor(c => c.IBAN)
                 .NotEmpty();
                 // Todo: Validierung IBAN (Regex)


            RuleFor(c => c.BIC)
                .NotEmpty()
                .When(dto => !dto.IBAN.StartsWith("de", StringComparison.InvariantCultureIgnoreCase));
                 // Todo: Validierung BIC (Regex)


            RuleFor(c => c.CommentInner)
                .GeneralText(500);

        }
    }
}
