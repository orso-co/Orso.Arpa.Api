using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
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
        public virtual ICollection<SelectValueMapping> SelectValueMappings { get; set; } = new HashSet<SelectValueMapping>();
        public virtual ICollection<InstrumentPart> InstrumentParts { get; set; } = new HashSet<InstrumentPart>();
    }
}
