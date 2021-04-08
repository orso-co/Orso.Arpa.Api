using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class Create
    {
        public class Command : ICreateCommand<Appointment>
        {
            public Guid? CategoryId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Name { get; set; }
            public string PublicDetails { get; set; }
            public string InternalDetails { get; set; }
            public Guid? StatusId { get; set; }
            public Guid? EmolumentId { get; set; }
            public Guid? EmolumentPatternId { get; set; }
            public Guid? ExpectationId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.EmolumentId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Emolument);

                RuleFor(d => d.EmolumentPatternId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.EmolumentPattern);

                RuleFor(d => d.ExpectationId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Expectation);

                RuleFor(d => d.StatusId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Status);

                RuleFor(d => d.CategoryId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Category);
            }
        }
    }
}
