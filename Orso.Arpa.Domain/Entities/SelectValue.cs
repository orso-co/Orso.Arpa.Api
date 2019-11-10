using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValue : BaseEntity
    {
        public SelectValue(Guid id) : base(id)
        {
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
