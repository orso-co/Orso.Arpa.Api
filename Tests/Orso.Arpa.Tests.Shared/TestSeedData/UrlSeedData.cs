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
            ArpaWebsite,
            OrsoWebsite,
            Google,
            GoogleDe,
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
                url.roleIds.Add(RoleSeedData.Staff.Id);
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
                url.roleIds.Add(RoleSeedData.Staff.Id);
                url.roleIds.Add(RoleSeedData.Performer.Id);
                return url;
            }
        }
        public static Url Google
        {
            get
            {
                var command = new Create.Command
                {
                    AnchorText = "Google",
                    Href = "https://www.google.com"
                };
                var url = new Url(Guid.Parse("fa8d667d-3bb6-4482-91ed-20754be6d539"), command);
                url.roleIds.Add(RoleSeedData.Performer.Id);
                return url;
            }
        }
        public static Url GoogleDe
        {
            get
            {
                var command = new Create.Command
                {
                    AnchorText = "Google",
                    Href = "https://www.google.com"
                };
                var url = new Url(Guid.Parse("fa8d667d-3bb6-4482-91ed-20754be6d539"), command);
                // url.rolesIds remains empty here
                return url;
            }
        }
    }
}
