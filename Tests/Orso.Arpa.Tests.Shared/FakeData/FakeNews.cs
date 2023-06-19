using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeNews
    {
        public static News ErsteNews
        {
            get
            {
                News news = NewsSeedData.ErsteNews;
                news.SetProperty(nameof(News.CreatedBy), "anonymous");
                news.SetProperty(nameof(News.CreatedAt), FakeDateTime.UtcNow);
                return news;
            }
        }

        public static News ZweiteNews
        {
            get
            {
                News news = NewsSeedData.ZweiteNews;
                news.SetProperty(nameof(News.CreatedBy), "anonymous");
                news.SetProperty(nameof(News.CreatedAt), FakeDateTime.UtcNow);
                return news;
            }
        }
    }
}
