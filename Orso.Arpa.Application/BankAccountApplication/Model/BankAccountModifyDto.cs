using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.BankAccountApplication.Model
{
    public class BankAccountModifyDto : IdFromRouteDto<BankAccountModifyBodyDto>
    {
        [FromRoute]
        public Guid BankAccountId { get; set; }
    }

    public class BankAccountModifyBodyDto
    {
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string CommentInner { get; set; }
        public Guid? StatusId { get; set; }
        public string AccountOwner { get; set; }
    }

    public class BankAccountModifyDtoMappingProfile : Profile
    {
        public BankAccountModifyDtoMappingProfile()
        {
            _ = CreateMap<BankAccountModifyDto, ModifyBankAccount.Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Iban, opt => opt.MapFrom(src => src.Body.Iban))
                .ForMember(dest => dest.Bic, opt => opt.MapFrom(src => src.Body.Bic))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.CommentInner))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BankAccountId))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Body.StatusId))
                .ForMember(dest => dest.AccountOwner, opt => opt.MapFrom(src => src.Body.AccountOwner));
        }
    }

    public class BankAccountModifyDtoValidator : IdFromRouteDtoValidator<BankAccountModifyDto, BankAccountModifyBodyDto>
    {
        public BankAccountModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new BankAccountModifyBodyDtoValidator());

            _ = RuleFor(dto => dto.BankAccountId)
                .NotEmpty();
        }
    }

    public class BankAccountModifyBodyDtoValidator : AbstractValidator<BankAccountModifyBodyDto>
    {
        public BankAccountModifyBodyDtoValidator()
        {
            _ = RuleFor(c => c.Iban)
                 .NotEmpty()
                 .Iban();

            _ = RuleFor(c => c.Bic)
                .NotEmpty()
                .When(dto => !dto.Iban.StartsWith("de", StringComparison.InvariantCultureIgnoreCase), ApplyConditionTo.CurrentValidator)
                .Bic()
                .When(dto => !string.IsNullOrWhiteSpace(dto.Bic), ApplyConditionTo.CurrentValidator);

            _ = RuleFor(c => c.CommentInner)
                .RestrictedFreeText(500);

            _ = RuleFor(c => c.AccountOwner)
                .PersonName(50);
        }
    }
}
