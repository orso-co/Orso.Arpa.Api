using System;
using Orso.Arpa.Domain.SelectValueCategories;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValueCategory : BaseEntity
    {
        public SelectValueCategory(Guid id, Create.Command command) : base(id)
        {
            Table = command.Table;
            Property = command.Property;
            Name = command.Name;
        }

        public string Table { get; private set; }
        public string Property { get; private set; }
        public string Name { get; private set; }
    }
}
