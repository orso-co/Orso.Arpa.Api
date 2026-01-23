using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class ModifyPersonMembership
    {
        public class Command : IModifyCommand<PersonMembership>
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public DateTime EntryDate { get; set; }
            public DateTime? ExitDate { get; set; }
            public decimal AnnualFee { get; set; }
            public Guid? SupportLevelId { get; set; }
            public Guid? MembershipStatusId { get; set; }
            public Guid? PaymentMethodId { get; set; }
            public Guid? PaymentFrequencyId { get; set; }
            public Guid? ClubId { get; set; }
            public string StaffComment { get; set; }
            public string PerformerComment { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .MustAsync(async (command, id, cancellation) => await arpaContext
                        .EntityExistsAsync<PersonMembership>(m => m.Id == id && m.PersonId == command.PersonId, cancellation))
                    .WithMessage("Membership could not be found")
                    .WithErrorCode("404");

                RuleFor(c => c.EntryDate)
                    .NotEmpty();

                RuleFor(c => c.AnnualFee)
                    .GreaterThanOrEqualTo(0);

                RuleFor(c => c.SupportLevelId)
                    .SelectValueMapping<Command, PersonMembership>(arpaContext, m => m.SupportLevel);

                RuleFor(c => c.MembershipStatusId)
                    .SelectValueMapping<Command, PersonMembership>(arpaContext, m => m.MembershipStatus);

                RuleFor(c => c.PaymentMethodId)
                    .SelectValueMapping<Command, PersonMembership>(arpaContext, m => m.PaymentMethod);

                RuleFor(c => c.PaymentFrequencyId)
                    .SelectValueMapping<Command, PersonMembership>(arpaContext, m => m.PaymentFrequency);

                RuleFor(c => c.ClubId)
                    .SelectValueMapping<Command, PersonMembership>(arpaContext, m => m.Club);
            }
        }
    }
}
