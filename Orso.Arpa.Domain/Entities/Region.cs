using System;

namespace Orso.Arpa.Domain.Entities
{
    public class Region : BaseEntity
    {
        public Region(Guid id) : base(id)
        {
        }

        public string Name { get; private set; }
    }
}
