using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Rooms;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class RoomSeedData
    {
        public static IList<Room> Rooms
        {
            get
            {
                return new List<Room>
                {
                    AulaWeiherhofSchule
                };
            }
        }

        public static Room AulaWeiherhofSchule
        {
            get
            {
                return new Room
                (
                    Guid.Parse("4f5767a8-0c2d-4bf0-8623-47f040be857b"),
                    new Create.Command
                    {
                        Building = "Anbau",
                        Floor = "EG",
                        Name = "Aula",
                        VenueId = VenueSeedData.WeiherhofSchule.Id
                    }
                );
            }
        }
    }
}
