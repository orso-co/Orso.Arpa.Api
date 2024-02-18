using System;
using System.Collections.Generic;
using Orso.Arpa.Domain._General.Model;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ClubDomain.Model
{
    public class Club : BaseEntity
    {
        public Club(Guid? id, CreateClub.Command command) : base(id)
        {
            Name = command.Name;
            Address = new Address(command);
        }

        protected Club()
        {
        }
        
        [CascadingSoftDelete]
        public virtual ICollection<ClubMembershipType> MembershipTypes { get; private set; } = new HashSet<ClubMembershipType>();
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}