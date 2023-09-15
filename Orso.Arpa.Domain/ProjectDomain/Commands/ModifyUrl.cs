using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.ProjectDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.ProjectDomain.Commands
{
    public static class ModifyUrl
    {
        public class Command : IModifyCommand<Url>
        {
            public Guid Id { get; set; }
            public string Href { get; set; }
            public string AnchorText { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Url>(arpaContext);
            }
        }
    }
}
