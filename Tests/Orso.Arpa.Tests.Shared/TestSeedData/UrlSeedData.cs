using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Projects;
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
                var command = new AddUrl.Command
                {
                    AnchorText = "Our great ARPA 2.0 Website",
                    Href = "https://arpa2.orso.co",
                    ProjectId = ProjectSeedData.RockingXMas.Id
                };
                var id = Guid.Parse("a009cea6-031c-4f0c-ac05-931b2826127d");
                var url = new Url(id, command);
                url.UrlRoles.Add(new(id, RoleSeedData.Staff.Id));
                return url;
            }
        }
        public static Url OrsoWebsite
        {
            get
            {
                var command = new AddUrl.Command
                {
                    AnchorText = "ORSO website",
                    Href = "https://www.orso.co",
                    ProjectId = ProjectSeedData.RockingXMas.Id
                };
                var id = Guid.Parse("2d7c6ac5-5b1c-42ef-9c3d-1edd3f46b33f");
                var url = new Url(id, command);
                url.UrlRoles.Add(new(id, RoleSeedData.Staff.Id));
                url.UrlRoles.Add(new(id, RoleSeedData.Performer.Id));
                return url;
            }
        }
        public static Url Google
        {
            get
            {
                var command = new AddUrl.Command
                {
                    AnchorText = "Google",
                    Href = "https://www.google.com",
                    ProjectId = ProjectSeedData.RockingXMas.Id,
                };
                var id = Guid.Parse("fa8d667d-3bb6-4482-91ed-20754be6d539");
                var url = new Url(id, command);
                url.UrlRoles.Add(new(id, RoleSeedData.Performer.Id));
                return url;
            }
        }
        public static Url GoogleDe
        {
            get
            {
                var command = new AddUrl.Command
                {
                    AnchorText = "Google DE",
                    Href = "https://www.google.de",
                    ProjectId = ProjectSeedData.HoorayForHollywood.Id
                };
                return new Url(Guid.Parse("be021bf0-db90-4a05-9d7c-0e98c9a3f893"), command);
            }
        }
    }
}
