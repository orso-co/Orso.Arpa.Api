using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Urls;
using Orso.Arpa.Persistence.Seed;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class UrlSeedData
    {
        public static IList<Url> Urls => new List<Url>
        {
            ArpaWebsiteUrl
        };

        public static Url ArpaWebsiteUrl
        {
            get
            {
                var command = new Create.Command {
                    AnchorText = "Our great arpa website",
                    Href = "https://orsoarpa.de"
                };
                var url = new Url(Guid.Parse("a009cea6-031c-4f0c-ac05-931b2826127d"), command);
                url.Roles.Add(RoleSeedData.Performer);
                return url;
            }
        }
    }
}
