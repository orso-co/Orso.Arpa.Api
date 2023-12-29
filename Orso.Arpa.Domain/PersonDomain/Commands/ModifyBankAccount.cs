using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class ModifyBankAccount
    {
        public class Command : IModifyCommand<BankAccount>
        {
            public Guid Id { get; set; }
            public string Iban { get; set; }
            public string Bic { get; set; }
            public string CommentInner { get; set; }
            public Guid? StatusId { get; set; }
            public Guid PersonId { get; set; }
            public string AccountOwner { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<BankAccount>(bankAccount => bankAccount.Id == id && bankAccount.PersonId == command.PersonId, cancellation))
                    .WithMessage("Bank account could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.Iban)
                    .MustAsync(async (command, iban, cancellation) => !(await arpaContext
                        .EntityExistsAsync<BankAccount>(bankAccount =>
                            bankAccount.Id != command.Id
                                && bankAccount.PersonId == command.PersonId
#pragma warning disable RCS1155, CA1862 // Use StringComparison when comparing strings.
                                && bankAccount.Iban.ToLower() == iban.ToLower(), cancellation)))
#pragma warning restore RCS1155, CA1862 // Use StringComparison when comparing strings.
                    .WithMessage("Bank account with this IBAN already taken");

                RuleFor(c => c.StatusId)
                    .SelectValueMapping<Command, BankAccount>(arpaContext, p => p.Status);
            }
        }
    }
}
