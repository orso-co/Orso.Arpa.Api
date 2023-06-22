using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeNews
    {
        public static News FirstNews
        {
            get
            {
                News news = NewsSeedData.FirstNews;
                news.SetProperty(nameof(News.CreatedBy), "anonymous");
                news.SetProperty(nameof(News.CreatedAt), FakeDateTime.UtcNow);
                return news;
            }
        }

        public static News SecondNews
        {
            get
            {
                News news = NewsSeedData.SecondNews;
                news.SetProperty(nameof(News.CreatedBy), "anonymous");
                news.SetProperty(nameof(News.CreatedAt), FakeDateTime.UtcNow);
                return news;
            }
        }
    }
}
