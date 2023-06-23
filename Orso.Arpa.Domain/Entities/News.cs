using System;
using Orso.Arpa.Domain.Logic.News;

namespace Orso.Arpa.Domain.Entities
{
    public class News : BaseEntity
    {
        public News(Guid? id, Create.Command command) : base(id)
        {
            Title = command.Title;
            Content = command.Content;
            Url = command.Url;
            Show = command.Show;
        }
        protected News() {}

        public void Update(Modify.Command command)
        {
            Title = command.Title;
            Content = command.Content;
            Url = command.Url;
            Show = command.Show;
        }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string Url { get; private set; }
        public bool Show { get; private set; }
    }
}
