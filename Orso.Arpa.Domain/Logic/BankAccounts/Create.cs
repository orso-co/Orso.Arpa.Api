using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.BankAccounts
{
    public static class Create
    {
        public class Command : ICreateCommand<BankAccount>
        {
            public string IBAN { get; set; }
            public string BIC { get; set; }
            public string CommentInner { get; set; }
            public Guid PersonId { get; set; }

        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonId)
                    .EntityExists<Command, Person>(arpaContext);

                RuleFor(c => c.IBAN)
                    .MustAsync(async (command, iban, cancellation) => !(await arpaContext
                        .EntityExistsAsync<BankAccount>(bankAccount =>
                            bankAccount.PersonId == command.PersonId
                                && bankAccount.IBAN.ToLower() == iban.ToLower(), cancellation)))
                    .WithMessage("Bankaccount with this IBAN already taken");
            }
        }
    }
}
