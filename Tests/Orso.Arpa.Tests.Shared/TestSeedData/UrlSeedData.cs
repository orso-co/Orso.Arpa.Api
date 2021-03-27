using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Urls;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class UrlSeedData
    {
        public static IList<Url> Urls => new List<Url>
        {
            ArpaWebsite,
            OrsoWebsite,
        };

        public static Url ArpaWebsite
        {
            get
            {
                var command = new Create.Command
                {
                    AnchorText = "Our great ARPA 2.0 Website",
                    Href = "https://arpa2.orso.co"
                };
                var url = new Url(Guid.Parse("a009cea6-031c-4f0c-ac05-931b2826127d"), command);
                //TODO url.Roles.Add(RoleSeedData.Performer);
                return url;
            }
        }
        public static Url OrsoWebsite
        {
            get
            {
                var command = new Create.Command
                {
                    AnchorText = "ORSO website",
                    Href = "https://www.orso.co"
                };
                var url = new Url(Guid.Parse("2d7c6ac5-5b1c-42ef-9c3d-1edd3f46b33f"), command);
                //TODO url.Roles.Add(RoleSeedData.Staff);
                return url;
            }
        }
    }
}
