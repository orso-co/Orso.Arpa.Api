using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Urls;

namespace Orso.Arpa.Domain.Entities
{
    public class Url : BaseEntity
    {
        public Url(Guid? id, Create.Command command) : base(id)
        {
            Href = command.Href;
            AnchorText = command.AnchorText;
            roleIds = command.roleIds;
        }

        internal Url(Guid? id) : base(id)
        {
        }

        [JsonConstructor]
        protected Url()
        {
        }

        public string Href { get; private set; }
        public string AnchorText { get; private set; }
        public IList<Guid> roleIds { get; private set; } = new List<Guid>();
    }
}
