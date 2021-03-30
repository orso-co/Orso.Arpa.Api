using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class Modify
    {
        public class Command : IModifyCommand<Url>
        {
            public Guid Id { get; set; }
            public string Href { get; private set; }
            public string AnchorText { get; private set; }
            public IList<Guid> roleIds { get; private set; } = new List<Guid>();
        }
    }
}
