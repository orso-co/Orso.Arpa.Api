using System;

namespace Orso.Arpa.Domain.Entities
{
    public class Region : BaseEntity
    {
        public Region(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
