using System;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.FinanceDomain.Model
{
    public class FinanceClubAccess : BaseEntity
    {
        public FinanceClubAccess(Guid? id, Guid userId, Guid clubId, string grantedBy) : base(id)
        {
            UserId = userId;
            ClubId = clubId;
            GrantedBy = grantedBy;
            GrantedAt = DateTime.UtcNow;
        }

        protected FinanceClubAccess()
        {
        }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public Guid ClubId { get; private set; }
        public virtual Club Club { get; private set; }

        public string GrantedBy { get; private set; }
        public DateTime GrantedAt { get; private set; }
    }
}
