using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class CreateBankAccount
    {
        public class Command : ICreateCommand<BankAccount>
        {
            public string Iban { get; set; }
            public string Bic { get; set; }
            public string CommentInner { get; set; }
            public Guid PersonId { get; set; }
            public string AccountOwner { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.Iban)
                    .MustAsync(async (command, iban, cancellation) => !(await arpaContext
                        .EntityExistsAsync<BankAccount>(bankAccount =>
                            bankAccount.PersonId == command.PersonId
#pragma warning disable RCS1155, CA1862 // Use StringComparison when comparing strings.
                                && bankAccount.Iban.ToLower() == iban.ToLower(), cancellation)))
#pragma warning restore RCS1155, CA1862 // Use StringComparison when comparing strings.
                    .WithMessage("Bank account with this IBAN already taken");
            }
        }
    }
}
