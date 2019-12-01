using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValue : BaseEntity
    {
        internal SelectValue(Guid? id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        protected SelectValue()
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
