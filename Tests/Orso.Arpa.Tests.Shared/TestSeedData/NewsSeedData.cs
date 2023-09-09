using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.NewsDomain.Commands;
using Orso.Arpa.Domain.NewsDomain.Model;

namespace Orso.Arpa.Tests.Shared.TestSeedData;

public static class NewsSeedData
{
    public static IList<News> News =>
        new List<News> { FirstNews, SecondNews };

    public static News FirstNews =>
        new(
            Guid.Parse("416981c5-2512-442f-8b2e-dd9364faf40f"),
            new CreateNews.Command
            {
                Title = "Titel der ersten Message",
                Content = "ErsteMessage",
                Url = "https://orsopolis.de",
                Show = true
            }
        );

    public static News SecondNews =>
        new(
            Guid.Parse("116232e3-f972-4d3e-bd98-5ead7b76cff8"),
            new CreateNews.Command
            {
                Title = "Titel der zweiten Message",
                Content = "ZweiteMessage",
                Url = "https://orso.co",
                Show = false
            }
        );
}
