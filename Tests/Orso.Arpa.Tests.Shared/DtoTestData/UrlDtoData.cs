using System;
using System.Collections.Generic;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Application.UrlApplication;
using Orso.Arpa.Tests.Shared.FakeData;

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

        public static UrlDto ArpaWebsite => new(
            Guid.Parse("a009cea6-031c-4f0c-ac05-931b2826127d"),
            "Our great ARPA 2.0 Website",
            "https://arpa2.orso.co",
            new List<RoleDto> { RoleDtoData.Staff },
            "anonymous",
            FakeDateTime.UtcNow);

        public static UrlDto OrsoWebsite => new(
            Guid.Parse("2d7c6ac5-5b1c-42ef-9c3d-1edd3f46b33f"),
            "ORSO website",
            "https://www.orso.co",
            new List<RoleDto> { RoleDtoData.Staff, RoleDtoData.Performer },
            "anonymous",
            FakeDateTime.UtcNow);

        public static UrlDto Google => new(
            Guid.Parse("fa8d667d-3bb6-4482-91ed-20754be6d539"),
            "Google",
            "https://www.google.com",
            new List<RoleDto> { RoleDtoData.Performer },
            "anonymous",
            FakeDateTime.UtcNow);

        public static UrlDto GoogleDe => new(
            Guid.Parse("be021bf0-db90-4a05-9d7c-0e98c9a3f893"),
            "Google DE",
            "https://www.google.de",
            new List<RoleDto>(),
            "anonymous",
            FakeDateTime.UtcNow
            );
    }
}
