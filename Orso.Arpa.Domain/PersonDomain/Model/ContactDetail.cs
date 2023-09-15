using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class ContactDetail : BaseEntity
    {
        public ContactDetail(Guid? id, CreateContactDetail.Command command) : base(id)
        {
            Key = command.Key;
            Value = command.Value;
            TypeId = command.TypeId;
            CommentTeam = command.CommentTeam;
            Preference = command.Preference;
            PersonId = command.PersonId;
        }

        public ContactDetail(Guid? id, CreateMyContactDetail.Command command) : base(id)
        {
            Key = command.Key;
            Value = command.Value;
            TypeId = command.TypeId;
            CommentInner = command.CommentInner;
            Preference = command.Preference;
            PersonId = command.PersonId;
        }

        protected ContactDetail() { }

        public void Update(ModifyContactDetails.Command command)
        {
            Key = command.Key;
            Value = command.Value;
            TypeId = command.TypeId;
            CommentTeam = command.CommentTeam;
            Preference = command.Preference;
        }

        public void Update(ModifyMyContactDetails.Command command)
        {
            Key = command.Key;
            Value = command.Value;
            TypeId = command.TypeId;
            CommentInner = command.CommentInner;
            Preference = command.Preference;
        }

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
