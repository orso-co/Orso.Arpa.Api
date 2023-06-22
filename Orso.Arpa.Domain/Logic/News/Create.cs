using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.News
{
    public static class Create
    {
        public class Command : ICreateCommand<Entities.News>
        {   public string Title { get; set; }
            public string Text { get; set; }
            public string Url { get; set; }
            public bool Show { get; set; }

        }
    }
}
