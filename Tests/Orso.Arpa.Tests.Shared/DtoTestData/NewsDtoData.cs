using System;
using System.Collections.Generic;
using Orso.Arpa.Application.NewsApplication;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.DtoTestData;

public static class NewsDtoData
{
    public static IList<NewsDto> News =>
        new List<NewsDto> { FirstNews, SecondNews };

    public static NewsDto FirstNews =>
        new()
        {
            Id = Guid.Parse("416981c5-2512-442f-8b2e-dd9364faf40f"),
            Title = "Titel der ersten Message",
            Content = "ErsteMessage",
            Url = "https://orsopolis.de",
            Show = true,
            CreatedBy = "anonymous",
            CreatedAt = FakeDateTime.UtcNow
        };

    public static NewsDto SecondNews =>
        new()
        {
            Id = Guid.Parse("116232e3-f972-4d3e-bd98-5ead7b76cff8"),
            Title = "Titel der zweiten Message",
            Content = "ZweiteMessage",
            Url = "https://orso.co",
            Show = false,
            CreatedBy = "anonymous",
            CreatedAt = FakeDateTime.UtcNow
        };
}
