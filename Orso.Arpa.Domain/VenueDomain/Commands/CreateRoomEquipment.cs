using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class CreateRoomEquipment
    {
        public class Command : ICreateCommand<RoomEquipment>
        {
            public Guid RoomId { get; set; }
            public Guid EquipmentId { get; set; }
            public int? Quantity { get; set; }
            public string Description { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.RoomId)
                    .EntityExists<Command, Room>(arpaContext);
                RuleFor(c => c.EquipmentId)
                    .SelectValueMapping<Command, RoomEquipment>(arpaContext, a => a.Equipment);
            }
        }
    }
}