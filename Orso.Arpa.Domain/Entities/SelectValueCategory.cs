using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValueCategory : BaseEntity
    {
        public SelectValueCategory(Guid id) : base(id)
        {
        }

        public string Table { get; private set; }
        public string Property { get; private set; }
        public string Name { get; private set; }
    }
}
