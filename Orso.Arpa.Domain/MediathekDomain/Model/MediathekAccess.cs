using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Model
{
    public class MediathekAccess : BaseEntity
    {
        public MediathekAccess(Guid? id, Guid personId, string grantedBy, string notes) : base(id)
        {
            PersonId = personId;
            GrantedBy = grantedBy;
            GrantedAt = DateTime.UtcNow;
            IsActive = true;
            Notes = notes;
        }

        public MediathekAccess()
        {
        }

        public Guid PersonId { get; private set; }
        public string GrantedBy { get; private set; }
        public DateTime GrantedAt { get; private set; }
        public bool IsActive { get; private set; }
        public string Notes { get; private set; }

        public virtual Person Person { get; private set; }

        public void Revoke()
        {
            IsActive = false;
        }

        public void Reactivate(string grantedBy)
        {
            IsActive = true;
            GrantedBy = grantedBy;
            GrantedAt = DateTime.UtcNow;
        }
    }
}
