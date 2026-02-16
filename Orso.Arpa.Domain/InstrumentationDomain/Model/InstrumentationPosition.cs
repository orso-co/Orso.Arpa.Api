using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Model
{
    public class InstrumentationPosition : BaseEntity
    {
        public InstrumentationPosition(Guid? id, Guid instrumentationId, Guid sectionId, int quantity,
            Guid? qualificationId, int sortOrder, string label, string comment) : base(id)
        {
            InstrumentationId = instrumentationId;
            SectionId = sectionId;
            Quantity = quantity;
            QualificationId = qualificationId;
            SortOrder = sortOrder;
            Label = label;
            Comment = comment;
        }

        [JsonConstructor]
        protected InstrumentationPosition() { }

        public Guid InstrumentationId { get; set; }
        public virtual Instrumentation Instrumentation { get; set; }

        public Guid SectionId { get; set; }
        public virtual Section Section { get; set; }

        public int Quantity { get; set; }

        public Guid? QualificationId { get; set; }
        public virtual SelectValueMapping Qualification { get; set; }

        public int SortOrder { get; set; }
        public string Label { get; set; }
        public string Comment { get; set; }

        [CascadingSoftDelete]
        public virtual ICollection<InstrumentationPositionDoubling> Doublings { get; set; } = new HashSet<InstrumentationPositionDoubling>();
    }
}
