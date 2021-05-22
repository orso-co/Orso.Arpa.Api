using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Persons
{
    public static class Create
    {
        public class Command : ICreateCommand<Person>
        {
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string AboutMe { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
            }
        }
    }
}
