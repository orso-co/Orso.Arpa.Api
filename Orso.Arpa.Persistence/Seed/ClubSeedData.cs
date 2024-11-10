using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.ClubDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class ClubSeedData
    {
        public static List<Club> Clubs => [
            OrsoFreiburg,
            OrsoBerlin,
            OrsoStuttgart
        ];

        public static Club OrsoFreiburg => new(
            Guid.Parse("ef604736-1e4f-4ee1-80c2-41e39f344239"),
            new CreateClub.Command
            {
                Name = "Orso Freiburg e. V.",
               /* Address1 = "Schwarzwaldstraße 9–11",
                Zip = "79117",
                City = "Freiburg im Breisgau",
                UrbanDistrict = "Wiehre",
                State = "Baden-Württemberg" */
            }
        );

        public static Club OrsoBerlin => new(
            Guid.Parse("1fbfad36-080a-4e87-a022-9b3d441e81b9"),
            new CreateClub.Command
            {
                Name = "Orso Berlin e. V.",
               /* Address1 = "Hauptstraße 134",
                Zip = "10827",
                City = "Berlin",
                UrbanDistrict = "Schöneberg",
                State = "Berlin" */
            }
        );


        public static Club OrsoStuttgart => new(
            Guid.Parse("03aa55e4-d878-4dc3-a926-f5ebe12a7347"),
            new CreateClub.Command
            {
                Name = "Orso Stuttgart e. V.",
              /*  Address1 = "Waiblinger Straße 144c",
                Zip = "70374",
                City = "Stuttgart",
                State = "Baden-Württemberg" */
            }
        );
    }
}
