using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class Create
    {
        public class Command : ICreateCommand<Url>
        {
            public string Href { get; set; }
            public string AnchorText { get; set; }
            public Guid ProjectId { get; set; }
        }
    }
}
