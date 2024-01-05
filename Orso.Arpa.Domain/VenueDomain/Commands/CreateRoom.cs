using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class CreateRoom
    {
        public class Command : ICreateCommand<Room>
        {
            public string Name { get; set; }
            public Guid VenueId { get; set; }
            public string Building { get; set; }
            public string Floor { get; set; }
            public CeilingHeight? CeilingHeight { get; set; }
            public Guid? CapacityId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.VenueId)
                    .EntityExists<Command, Venue>(arpaContext);
                RuleFor(c => c.CapacityId)
                    .SelectValueMapping<Command, Room>(arpaContext, a => a.Capacity);
            }
        }
    }
}
