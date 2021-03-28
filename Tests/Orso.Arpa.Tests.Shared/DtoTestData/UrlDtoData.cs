using System.Collections.Generic;
using Orso.Arpa.Application.ProjectApplication;

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
                    AnchorText = "Our great ARPA 2.0 Website",
                    Href = "https://arpa2.orso.co"
                    //TODO Roles =
                };
            }
        }

        public static UrlDto OrsoWebsite
        {
            get
            {
                return new UrlDto
                {
                    AnchorText = "ORSO website",
                    Href = "https://www.orso.co"
                    //TODO Roles =
                };
            }
        }
        public static UrlDto Google
        {
            get
            {
                return new UrlDto
                {
                    AnchorText = "Google",
                    Href = "https://www.google.com"
                    //TODO Roles =
                };
            }
        }
        public static UrlDto GoogleDe
        {
            get
            {
                return new UrlDto
                {
                    AnchorText = "Google DE",
                    Href = "https://www.google.de"
                    //TODO Roles =
                };
            }
        }
    }
}
