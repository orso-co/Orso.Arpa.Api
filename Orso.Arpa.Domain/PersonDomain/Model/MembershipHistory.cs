using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class MembershipHistory : BaseEntity
    {
        public MembershipHistory(Guid? id, CreateMembershipHistory.Command command) : base(id)
        {
            Year = command.Year;
            Amount = command.Amount;
            IsReduced = command.IsReduced;
            Comment = command.Comment;
            PersonMembershipId = command.PersonMembershipId;
        }

        protected MembershipHistory() { }

        public void Update(ModifyMembershipHistory.Command command)
        {
            Year = command.Year;
            Amount = command.Amount;
            IsReduced = command.IsReduced;
            Comment = command.Comment;
        }

        public int Year { get; private set; }
        public decimal Amount { get; private set; }
        public bool IsReduced { get; private set; }
        public string Comment { get; private set; }

        // PersonMembership relationship (1:n)
        public Guid PersonMembershipId { get; private set; }
        public virtual PersonMembership PersonMembership { get; private set; }
    }
}
