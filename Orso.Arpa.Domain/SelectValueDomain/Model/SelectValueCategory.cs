using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Model
{
    public class SelectValueCategory : BaseEntity
    {
        internal SelectValueCategory(Guid id, string table, string property, string name) : base(id)
        {
            Table = table;
            Property = property;
            Name = name;
        }

        protected SelectValueCategory()
        {
        }

        public string Table { get; private set; }
        public string Property { get; private set; }
        public string Name { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<SelectValueMapping> SelectValueMappings { get; set; } = new HashSet<SelectValueMapping>();
    }
}
