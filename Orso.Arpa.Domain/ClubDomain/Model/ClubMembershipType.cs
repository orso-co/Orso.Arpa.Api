using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ClubDomain.Model
{
    public class ClubMembershipType : BaseEntity
    {
        public ClubMembershipType(Guid? id, CreateClubMembershipType.Command command) : base(id)
        {
            ClubId = command.ClubId;
            Name = command.Name;
            Description = command.Description;
            TerminationPeriodInMonths = command.TerminationPeriodInMonths;
        }
        
        public Guid ClubId { get; set; }
        public virtual Club Club { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TerminationPeriodInMonths { get; set; }   
                
        [CascadingSoftDelete]
        public virtual ICollection<ClubMembershipSubType> SubTypes { get; private set; } = new HashSet<ClubMembershipSubType>();
    }
}