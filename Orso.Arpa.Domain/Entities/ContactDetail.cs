using System;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Domain.Entities
{
    public class ContactDetail : BaseEntity
    {
        public ContactDetailKey Key { get; private set; }
        public string Value { get; private set; }
        public Guid? TypeId { get; private set; }
        public virtual SelectValueMapping Type { get; private set; }
        public string CommentInner { get; private set; }
        public string CommentTeam { get; private set; }
        public byte Preference { get; private set; }
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
    }
}
