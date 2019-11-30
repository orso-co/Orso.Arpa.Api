using System;

namespace Orso.Arpa.Domain.Entities
{
    public class Region : BaseEntity
    {
        internal Region(Guid? id, string name) : base(id)
        {
            Name = name;
        }

        private Region()
        {
        }

        public string Name { get; private set; }
    }
}
