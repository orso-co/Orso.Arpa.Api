using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.NewsDomain.Commands;

namespace Orso.Arpa.Domain.NewsDomain.Model;

public class News : BaseEntity
{
    public News(Guid id, CreateNews.Command command) : base(id)
    {
        Title = command.Title;
        Content = command.Content;
        Url = command.Url;
        Show = command.Show;
    }

    protected News() { }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Url { get; private set; }
    public bool Show { get; private set; }

    public void Update(ModifyNews.Command command)
    {
        Title = command.Title;
        Content = command.Content;
        Url = command.Url;
        Show = command.Show;
    }
}
