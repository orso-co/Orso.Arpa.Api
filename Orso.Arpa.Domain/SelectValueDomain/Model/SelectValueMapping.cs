using System;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Model
{
    public class SelectValueMapping : BaseEntity
    {
        public SelectValueMapping(Guid id, Guid selectValueCategoryId, Guid selectValueId, int? sortOrder = null) : base(id)
        {
            SelectValueCategoryId = selectValueCategoryId;
            SelectValueId = selectValueId;
            SortOrder = sortOrder;
        }

        protected SelectValueMapping()
        {
        }

        public Guid SelectValueId { get; private set; }
        public virtual SelectValue SelectValue { get; private set; }
        public Guid SelectValueCategoryId { get; private set; }
        public virtual SelectValueCategory SelectValueCategory { get; private set; }
        public int? SortOrder { get; private set; }

        public override string ToString()
        {
            return SelectValue?.ToString();
        }
    }
}
