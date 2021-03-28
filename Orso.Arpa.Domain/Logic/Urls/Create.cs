using System;
using System.Collections.Generic;
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
            public IList<Guid> roleIds { get; private set; } = new List<Guid>();
        }
    }
}
