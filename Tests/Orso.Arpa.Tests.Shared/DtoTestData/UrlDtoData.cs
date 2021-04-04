using System;
using System.Collections.Generic;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Application.UrlApplication;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UrlDtoData
    {
        public static IList<UrlDto> Urls
        {
            get
            {
                return new List<UrlDto>
                {
                    ArpaWebsite,
                    OrsoWebsite,
                    Google,
                    GoogleDe,
                };
            }
        }

        public static UrlDto ArpaWebsite
        {
            get
            {
                return new UrlDto
                {
                    Id = Guid.Parse("a009cea6-031c-4f0c-ac05-931b2826127d"),
                    AnchorText = "Our great ARPA 2.0 Website",
                    Href = "https://arpa2.orso.co",
                    Roles = new List<RoleDto> { RoleDtoData.Staff },
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static UrlDto OrsoWebsite
        {
            get
            {
                return new UrlDto
                {
                    Id = Guid.Parse("2d7c6ac5-5b1c-42ef-9c3d-1edd3f46b33f"),
                    AnchorText = "ORSO website",
                    Href = "https://www.orso.co",
                    Roles = new List<RoleDto> { RoleDtoData.Staff, RoleDtoData.Performer },
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }
        public static UrlDto Google
        {
            get
            {
                return new UrlDto
                {
                    Id = Guid.Parse("fa8d667d-3bb6-4482-91ed-20754be6d539"),
                    AnchorText = "Google",
                    Href = "https://www.google.com",
                    Roles = new List<RoleDto> { RoleDtoData.Performer },
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }
        public static UrlDto GoogleDe
        {
            get
            {
                return new UrlDto
                {
                    Id = Guid.Parse("be021bf0-db90-4a05-9d7c-0e98c9a3f893"),
                    AnchorText = "Google DE",
                    Href = "https://www.google.de",
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                    // RolesIds remains empty here
                };
            }
        }
    }
}
