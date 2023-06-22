using System;
using FluentValidation;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.News
{
    public static class Modify
    {
        public class Command : IModifyCommand<Entities.News>
        {
            public Guid Id { get; set; }
            public string NewsTitle { get; set; }
            public string NewsText { get; set; }
            public string Url { get; set; }
            public bool Show { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.Id)
                    .EntityExists<Command, Entities.News>(arpaContext);

            }
        }
    }
}

