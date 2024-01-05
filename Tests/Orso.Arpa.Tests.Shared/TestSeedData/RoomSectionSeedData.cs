using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Enums;
using Orso.Arpa.Domain.VenueDomain.Model;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class RoomSectionSeedData
    {
        public static IList<RoomSection> RoomSections
        {
            get
            {
                return new List<RoomSection>
                {
                    AulaWeiherhofSchulePiano
                };
            }
        }

        public static RoomSection AulaWeiherhofSchulePiano
        {
            get
            {
                return new RoomSection(Guid.Parse("b68a15ac-2c98-45aa-8655-c17388771783"), new CreateRoomSection.Command {
                    RoomId = RoomSeedData.AulaWeiherhofSchule.Id,
                    SectionId = SectionSeedData.Piano.Id,
                    Quantity = 1
                });
            }
        }
    }
}
