using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class ModifyRoom
    {
        public class Command : IModifyCommand<Room>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Building { get; set; }
            public string Floor { get; set; }
            public CeilingHeight? CeilingHeight { get; set; }
            public Guid? CapacityId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, Room>(arpaContext);

                RuleFor(c => c.CapacityId)
                    .SelectValueMapping<Command, Room>(arpaContext, a => a.Capacity);
            }
        }
    }
}