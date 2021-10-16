using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Delete;

namespace Orso.Arpa.Domain.Logic.MyContactDetails
{
    public static class Delete
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
