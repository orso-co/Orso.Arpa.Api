using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class VenueSeedData
    {
        public static IList<Venue> Venues
        {
            get
            {
                return new List<Venue>
                {
                    WeiherhofSchule
                };
            }
        }

        public static Venue WeiherhofSchule
        {
            get
            {
                return new Venue(
                    Guid.Parse("54eb30ff-6ea3-4026-8a49-5f149c8ec7e1"),
                    new CreateVenue.Command
                    {
                        Name = "Weiherhof Schule",
                        Address1 = "Schlüsselstraße 5",
                        Zip = "79104",
                        City = "Freiburg",
                        Description = "Proberäume",
                        Country = "Deutschland",
                        UrbanDistrict = "Herdern",
                        State = "Baden-Württemberg",
                        AddressId = Guid.Parse("9dfd22c2-41c6-463c-a4cd-334215584d56")
                    });
            }
        }
    }
}
