using Orso.Arpa.Domain.NewsDomain.Model;
using static Orso.Arpa.Domain.General.GenericHandlers.Create;

namespace Orso.Arpa.Domain.NewsDomain.Commands
{
    public static class CreateNews
    {
        public class Command : ICreateCommand<News>
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Url { get; set; }
            public bool Show { get; set; }

        }
    }
}
