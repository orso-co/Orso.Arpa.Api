using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValueCategory : BaseEntity
    {
        public SelectValueCategory(Guid id, string table, string property, string name) : base(id)
        {
            Table = table;
            Property = property;
            Name = name;
        }

        public string Table { get; private set; }
        public string Property { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<SelectValueMapping> SelectValueMappings { get; set; } = new HashSet<SelectValueMapping>();
    }
}
