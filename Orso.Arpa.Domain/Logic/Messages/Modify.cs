using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Messages
{
    public static class Modify
    {
        public class Command : IModifyCommand<Message>
        {
            public Guid Id { get; set; }
            public string MessageText { get; set; }
            public string Url { get; set; }
            public bool Show { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (id, cancellation) => await arpaContext.EntityExistsAsync<Message>(message => message.Id == id, cancellation))
                    .WithMessage("Message could not be found")
                    .WithErrorCode("404");
            }
        }
    }
}

