using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class PersonSection : BaseEntity
    {
        public PersonSection(Guid? id, Person person, Section section) : base(id)
        {
            Person = person;
            Section = section;
        }

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
