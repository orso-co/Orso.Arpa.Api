using System;

namespace Orso.Arpa.Domain.Entities
{
    public class PersonSection
    {
        public PersonSection(Guid personId, Guid sectionId)
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
