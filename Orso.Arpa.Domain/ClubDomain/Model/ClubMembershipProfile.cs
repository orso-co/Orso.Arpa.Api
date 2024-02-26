using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.ClubDomain.Model
{
    public class ClubMembershipProfile : BaseEntity
    {
        protected ClubMembershipProfile() {}

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid ClubId { get; set; }
        public virtual Club Club { get; set; }
        public DateTime? DeviatingMembershipTerminationDate { get; private set; }
        public string ReasonForDeviatingMembershipTerminationDate { get; private set; }
        public DateTime? MembershipTerminationDate { get; private set; }
        public string TerminationReason { get; private set; }
        public DateTime JoiningDate { get; private set; }
        
        [CascadingSoftDelete]
        public virtual ICollection<ClubMembershipProfileData> MembershipHistory { get; set; } = new HashSet<ClubMembershipProfileData>();
    }
}