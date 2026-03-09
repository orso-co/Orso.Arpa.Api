using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;

namespace Orso.Arpa.Domain.ProjectDomain.Model
{
    public class Url : BaseEntity
    {
        public Url(Guid id, CreateUrl.Command command) : base(id)
        {
            Href = command.Href;
            AnchorText = command.AnchorText;
            ProjectId = command.ProjectId;
        }

        internal Url(Guid id) : base(id)
        {
        }

        [JsonConstructor]
        protected Url()
        {
        }

        public void Update(ModifyUrl.Command command)
        {
            Href = command.Href;
            AnchorText = command.AnchorText;
        }

        public string Href { get; private set; }
        public string AnchorText { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<UrlRole> UrlRoles { get; private set; } = new HashSet<UrlRole>();
        public Guid ProjectId { get; private set; }
        public virtual Project Project { get; private set; }
    }
}
