using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ClubDomain.Model
{
    public class ClubMembershipSubType : BaseEntity
    {
        public ClubMembershipSubType(Guid id, CreateClubMembershipSubType.Command command) : base(id)
        {
            MemberhsipTypeId = command.MemberhsipTypeId;
            Name = command.Name;
            Advantages = command.Advantages;
            Prerequisites = command.Prerequisites;
        }

        protected ClubMembershipSubType() {}

        public Guid MemberhsipTypeId { get; set; }
        public virtual ClubMembershipType MembershipType { get; set; }
        public string Name { get; set; }
        public string Advantages { get; set; }
        public string Prerequisites { get; set; }

        [CascadingSoftDelete]
        public virtual ICollection<ClubMembershipContribution> ContributionHistory { get; private set; } = new HashSet<ClubMembershipContribution>();
    }
}
