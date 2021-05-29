using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValueMapping : BaseEntity
    {
        public SelectValueMapping(Guid? id, Guid selectValueCategoryId, Guid selectValueId) : base(id)
        {
            SelectValueCategoryId = selectValueCategoryId;
            SelectValueId = selectValueId;
        }

        protected SelectValueMapping()
        {
        }

        public Guid SelectValueId { get; private set; }
        public virtual SelectValue SelectValue { get; private set; }
        public Guid SelectValueCategoryId { get; private set; }
        public virtual SelectValueCategory SelectValueCategory { get; private set; }
    }
}
