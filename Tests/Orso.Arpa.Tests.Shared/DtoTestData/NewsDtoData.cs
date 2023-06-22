using System;
using System.Collections.Generic;
using Orso.Arpa.Application.NewsApplication;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class NewsDtoData
    {
        public static IList<NewsDto> News
        {
            get
            {
                return new List<NewsDto>
                {
                    FirstNews,
                    SecondNews
                };
            }
        }

        public static NewsDto FirstNews
        {
            get
            {
                return new NewsDto
                {
                    Id = Guid.Parse("416981c5-2512-442f-8b2e-dd9364faf40f"),
                    NewsText = "ErsteMessage",
                    Url = "https://orsopolis.de",
                    Show = true,
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static NewsDto SecondNews
        {
            get
            {
                return new NewsDto
                {
                    Id = Guid.Parse("116232e3-f972-4d3e-bd98-5ead7b76cff8"),
                    NewsText = "ZweiteMessage",
                    Url = "https://orso.co",
                    Show = false,
                    CreatedBy = "anonymous",
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

    }
}
