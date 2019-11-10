using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValueMap : BaseEntity
    {
        public SelectValueMap(Guid id) : base(id)
        {
        }

        public Guid SelectValueId { get; private set; }
        public virtual SelectValue SelectValue { get; private set; }
        public Guid SelectValueCategoryId { get; private set; }
        public virtual SelectValueCategory SelectValueCategory { get; private set; }
    }
}
