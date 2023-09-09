using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Delete;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class DeleteMyContactDetail
    {
        public class Command : IDeleteCommand<ContactDetail>
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<ContactDetail>(cd => cd.Id == id && cd.PersonId == command.PersonId, cancellation))
                    .WithMessage("Contact Detail could not be found")
                    .WithErrorCode("404");
            }
        }
    }
}
