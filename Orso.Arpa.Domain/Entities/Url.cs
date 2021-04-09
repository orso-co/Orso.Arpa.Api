using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Projects;

namespace Orso.Arpa.Domain.Entities
{
    public class Url : BaseEntity
    {
        public Url(Guid? id, AddUrl.Command command) : base(id)
        {
            Href = command.Href;
            AnchorText = command.AnchorText;
            ProjectId = command.ProjectId;
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
        public virtual ICollection<UrlRole> UrlRoles { get; private set; } = new HashSet<UrlRole>();
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
    }
}
