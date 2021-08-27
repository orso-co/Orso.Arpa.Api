using System;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Primitives;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.BankAccounts
{
    public static class Modify
    {
        public class Command : IModifyCommand<BankAccount>
        {
            public Guid Id { get; set; }
            public string IBAN { get; set; }
            public string BIC { get; set; }
            public string CommentInner { get; set; }
            public Guid? StatusId { get; set; }
            public Guid PersonId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, BankAccount>()
                    .ForMember(dest => dest.IBAN, opt => opt.MapFrom(src => src.IBAN))
                    .ForMember(dest => dest.BIC, opt => opt.MapFrom(src => src.BIC))
                    .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                    .ForMember(dest => dest.CommentInner, opt => opt.MapFrom(src => src.CommentInner))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext.EntityExistsAsync<BankAccount>(bankAccount => bankAccount.Id == id && bankAccount.PersonId == command.PersonId, cancellation))
                    .WithMessage("BankAccount could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.IBAN)
                    .MustAsync(async (command, iban, cancellation) => !(await arpaContext
                        .EntityExistsAsync<BankAccount>(bankAccount =>
                            bankAccount.Id != command.Id
                                && bankAccount.PersonId == command.PersonId
                                && bankAccount.IBAN.ToLower() == iban.ToLower(), cancellation)))
                    .WithMessage("Bankaccount with this IBAN already taken");

                RuleFor(c => c.StatusId)
                    .SelectValueMapping<Command, BankAccount>(arpaContext, p => p.Status);
            }
        }
    }
}
