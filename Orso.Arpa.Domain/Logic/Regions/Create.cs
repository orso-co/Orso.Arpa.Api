using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Regions
{
    public static class Create
    {
        public class Command : ICreateCommand<Region>
        {
            public string Name { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IReadOnlyRepository repository)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(c => c.Name)
                    .Must(name => !repository.Exists<Region>(r => r.Name == name))
                    .WithMessage("A region with the requested name does already exist");
            }
        }
    }
}
