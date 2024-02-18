using System;
using Orso.Arpa.Domain._General.Interfaces;
using Orso.Arpa.Domain.ClubDomain.Commands;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ClubDomain.Model
{
    public class ClubMembershipContribution : BaseEntity, IVersionedEntity
    {
        public ClubMembershipContribution(Guid? id, CreateClubMembershipContribution.Command command) : base(id)
        {
            ContributionPerYearInEuro = command.ContributionPerYearInEuro;
            ValidFrom = command.ValidFrom;
            VoucherPerConcertInPercent = command.VoucherPerConcertInPercent;
            MembershipSubTypeId = command.MembershipSubTypeId;
        }
        
        public decimal ContributionPerYearInEuro { get; set; }
        public DateTime ValidFrom { get; set; }
        public int VoucherPerConcertInPercent { get; set; }
        public Guid MembershipSubTypeId { get; set; }
        public virtual ClubMembershipSubType MembershipSubType { get; set; }
    }
}