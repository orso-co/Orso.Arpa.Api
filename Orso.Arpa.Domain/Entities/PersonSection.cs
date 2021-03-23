using System;

namespace Orso.Arpa.Domain.Entities
{
    public class PersonSection : BaseEntity
    {
        public PersonSection(Guid? id, Guid personId, Guid sectionId) : base(id)
        {
            PersonId = personId;
            SectionId = sectionId;
        }

        public PersonSection()
        {
        }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
    }
}
