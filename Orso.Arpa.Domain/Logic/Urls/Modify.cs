using System;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class Modify
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
