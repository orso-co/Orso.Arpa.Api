using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.News
{
    public static class Create
    {
        public class Command : ICreateCommand<Entities.News>
        {   public string NewsTitle { get; set; }
            public string NewsText { get; set; }
            public string Url { get; set; }
            public bool Show { get; set; }

        }
    }
}
