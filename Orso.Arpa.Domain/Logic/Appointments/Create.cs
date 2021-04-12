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
            public Guid? SalaryId { get; set; }
            public Guid? SalaryPatternId { get; set; }
            public Guid? ExpectationId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.SalaryId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.Salary);

                RuleFor(d => d.SalaryPatternId)
                    .SelectValueMapping<Command, Appointment>(arpaContext, a => a.SalaryPattern);

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
