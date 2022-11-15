using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Appointments
{
    public static class Modify
    {
        public class Command : IModifyCommand<Appointment>
        {
            public Guid Id { get; set; }
            public Guid? CategoryId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Name { get; set; }
            public string PublicDetails { get; set; }
            public string InternalDetails { get; set; }
            public AppointmentStatus? Status { get; set; }
            public Guid? SalaryId { get; set; }
            public Guid? SalaryPatternId { get; set; }
            public Guid? ExpectationId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext);

                _ = RuleFor(d => d.SalaryId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Salary);

                _ = RuleFor(d => d.SalaryPatternId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.SalaryPattern);

                _ = RuleFor(d => d.ExpectationId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Expectation);

                _ = RuleFor(d => d.CategoryId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Category);
            }
        }
    }
}
