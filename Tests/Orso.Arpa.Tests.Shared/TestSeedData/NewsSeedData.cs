using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.News;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class NewsSeedData
    {
        public static IList<News> News
        {
            get
            {
                return new List<News>
                {
                    FirstNews,
                    SecondNews
                };
            }
        }

        public static News FirstNews
        {
            get
            {
                return new News
                (
                 Guid.Parse("416981c5-2512-442f-8b2e-dd9364faf40f"),
                    new Create.Command
                    {   NewsTitle = "This is the first news title!",
                        NewsText = "ErsteMessage",
                        Url = "https://orsopolis.de",
                        Show = true
                    }
                );
            }
        }
        public static News SecondNews
        {
            get
            {
                return new News
                (
                    Guid.Parse("116232e3-f972-4d3e-bd98-5ead7b76cff8"),
                    new Create.Command
                    {
                            NewsTitle = "This is the second news title!",
                        NewsText = "ZweiteMessage",
                        Url = "https://orso.co",
                        Show = false
                    }
                );
            }
        }
    }
}
