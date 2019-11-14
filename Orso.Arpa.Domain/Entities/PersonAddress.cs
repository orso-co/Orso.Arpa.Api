using System;

namespace Orso.Arpa.Domain.Entities
{
    public class PersonAddress : Address
    {
        public PersonAddress(Guid id) : base(id)
        {
        }

        public Guid? PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid? TypeId { get; private set; }
        public virtual SelectValueMapping Type { get; private set; }
    }
}
