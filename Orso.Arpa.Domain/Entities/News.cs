using System;
using Orso.Arpa.Domain.Logic.News;

namespace Orso.Arpa.Domain.Entities
{
    public class News : BaseEntity
    {
        public News(Guid? id, Create.Command command) : base(id)
        {
            NewsTitle = command.NewsTitle;
            NewsText = command.NewsText;
            Url = command.Url;
            Show = command.Show;
        }
        protected News() {}

        public void Update(Modify.Command command)
        {
            NewsTitle = command.NewsTitle;
            NewsText = command.NewsText;
            Url = command.Url;
            Show = command.Show;
        }
        public string NewsTitle { get; private set; }
        public string NewsText { get; private set; }
        public string Url { get; private set; }
        public bool Show { get; private set; }
    }
}
