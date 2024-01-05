using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class ModifyRoomSection
    {
        public class Command : IModifyCommand<RoomSection>
        {
            public Guid Id { get; set; }
            public int? Quantity { get; set; }
            public string Description { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, RoomSection>(arpaContext);
            }
        }
    }
}