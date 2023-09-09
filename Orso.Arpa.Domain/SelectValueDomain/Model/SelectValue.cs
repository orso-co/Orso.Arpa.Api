using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Model
{
    public class SelectValue : BaseEntity
    {
        public SelectValue(Guid? id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        protected SelectValue()
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<SelectValueMapping> SelectValueMappings { get; set; } = new HashSet<SelectValueMapping>();

        [CascadingSoftDelete]
        public virtual ICollection<SelectValueSection> InstrumentParts { get; set; } = new HashSet<SelectValueSection>();

        public override string ToString()
        {
            return Name;
        }
    }
}
