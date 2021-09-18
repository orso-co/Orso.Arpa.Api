using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.BankAccounts.Modify;

namespace Orso.Arpa.Application.BankAccountApplication
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
            CreateMap<BankAccountModifyDto, Command>()
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
            RuleFor(d => d.Body)
                .SetValidator(new BankAccountModifyBodyDtoValidator());

            RuleFor(dto => dto.BankAccountId)
                .NotEmpty();
        }
    }

    public class BankAccountModifyBodyDtoValidator : AbstractValidator<BankAccountModifyBodyDto>
    {
        public BankAccountModifyBodyDtoValidator()
        {
            RuleFor(c => c.Iban)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .Iban();

            RuleFor(c => c.Bic)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Bic()
                .When(dto => !dto.Iban.StartsWith("de", StringComparison.InvariantCultureIgnoreCase));

            RuleFor(c => c.CommentInner)
                .GeneralText(500);

            RuleFor(c => c.AccountOwner)
                .GeneralText(50);
        }
    }
}
