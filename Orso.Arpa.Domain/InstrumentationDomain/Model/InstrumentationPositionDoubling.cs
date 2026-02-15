using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.InstrumentationDomain.Model
{
    public class InstrumentationPositionDoubling : BaseEntity
    {
        public InstrumentationPositionDoubling(Guid? id, Guid instrumentationPositionId, Guid sectionId) : base(id)
        {
            InstrumentationPositionId = instrumentationPositionId;
            SectionId = sectionId;
        }

        [JsonConstructor]
        protected InstrumentationPositionDoubling() { }

        public Guid InstrumentationPositionId { get; set; }
        public virtual InstrumentationPosition InstrumentationPosition { get; set; }

        public Guid SectionId { get; set; }
        public virtual Section Section { get; set; }
    }
}
