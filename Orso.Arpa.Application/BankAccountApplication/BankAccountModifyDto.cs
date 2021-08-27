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
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public string CommentInner { get; set; }
        public Guid? StatusId { get; set; }
    }
    public class BankAccountModifyDtoMappingProfile : Profile
    {
        public BankAccountModifyDtoMappingProfile()
        {
            CreateMap<BankAccountModifyDto, Command>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IBAN, opt => opt.MapFrom(src => src.Body.IBAN))
                .ForMember(dest => dest.BIC, opt => opt.MapFrom(src => src.Body.BIC))
                .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.Body.CommentInner))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BankAccountId))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Body.StatusId));

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
