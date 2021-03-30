using System;
using System.Linq;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeUrls
    {
        public static Url ArpaWebsite
        {
            get
            {
                Url url = UrlSeedData.ArpaWebsite;
                url.SetProperty(nameof(Url.CreatedBy), "anonymous");
                url.SetProperty(nameof(Url.CreatedAt), new DateTime(2021, 1, 1));
                foreach (UrlRole urlRole in url.UrlRoles)
                {
                    urlRole.SetProperty(nameof(UrlRole.Role), RoleSeedData.Roles.First(r => r.Id == urlRole.RoleId));
                }
                return url;
            }
        }

        public static Url OrsoWebsite
        {
            get
            {
                Url url = UrlSeedData.OrsoWebsite;
                url.SetProperty(nameof(Url.CreatedBy), "anonymous");
                url.SetProperty(nameof(Url.CreatedAt), new DateTime(2021, 1, 1));
                foreach (UrlRole urlRole in url.UrlRoles)
                {
                    urlRole.SetProperty(nameof(UrlRole.Role), RoleSeedData.Roles.First(r => r.Id == urlRole.RoleId));
                }
                return url;
            }
        }

        public static Url Google
        {
            get
            {
                Url url = UrlSeedData.Google;
                url.SetProperty(nameof(Url.CreatedBy), "anonymous");
                url.SetProperty(nameof(Url.CreatedAt), new DateTime(2021, 1, 1));
                foreach (UrlRole urlRole in url.UrlRoles)
                {
                    urlRole.SetProperty(nameof(UrlRole.Role), RoleSeedData.Roles.First(r => r.Id == urlRole.RoleId));
                }
                return url;
            }
        }
    }
}
