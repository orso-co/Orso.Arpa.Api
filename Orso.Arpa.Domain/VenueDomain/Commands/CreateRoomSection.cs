using System;
using FluentValidation;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.VenueDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.VenueDomain.Commands
{
    public static class CreateRoomSection
    {
        public class Command : ICreateCommand<RoomSection>
        {
            public Guid RoomId { get; set; }
            public Guid SectionId { get; set; }
            public int? Quantity { get; set; }
            public string Description { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.RoomId)
                    .EntityExists<Command, Room>(arpaContext);
                RuleFor(c => c.SectionId)
                    .EntityExists<Command, Section>(arpaContext);
            }
        }
    }
}