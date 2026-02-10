using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class CreateMembershipHistory
    {
        public class Command : ICreateCommand<MembershipHistory>
        {
            public int Year { get; set; }
            public decimal Amount { get; set; }
            public bool IsReduced { get; set; }
            public string Comment { get; set; }
            public Guid PersonMembershipId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.PersonMembershipId)
                    .EntityExists<Command, PersonMembership>(arpaContext);

                RuleFor(c => c.Year)
                    .InclusiveBetween(1900, 2100);

                RuleFor(c => c.Amount)
                    .GreaterThanOrEqualTo(0);

                RuleFor(c => c.Comment)
                    .MaximumLength(500);

                RuleFor(c => c)
                    .MustAsync(async (command, cancellation) => !(await arpaContext
                        .EntityExistsAsync<MembershipHistory>(h =>
                            h.PersonMembershipId == command.PersonMembershipId
                            && h.Year == command.Year, cancellation)))
                    .WithMessage("A history entry for this year already exists for this membership");
            }
        }
    }
}
