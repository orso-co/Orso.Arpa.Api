using System;
using Orso.Arpa.Domain._General.Interfaces;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.ClubDomain.Model
{
    /// <summary>
    /// Mitgliedschaftsdaten. Versioniert.
    /// </summary>
    public class ClubMembershipProfileData : BaseEntity, IVersionedEntity
    {
        protected ClubMembershipProfileData() {}

        public Guid ClubMembershipProfileId { get; private set; }
        public virtual ClubMembershipProfile ClubMembershipProfile { get; private set; }
        public Guid MembershipSubTypeId { get; private set; }
        public virtual ClubMembershipSubType MembershipSubType { get; private set; }
        public decimal? DeviatingAnnualContributionInEuro { get; private set; }
        public string ReasonForDeviatingAnnualContribution { get; private set; }
        public DateTime ValidFrom { get; private set; }
        //public Guid BankAccountId { get; set; }
        //public virtual PersonBankAccount BankAccount { get; private set; }
        public DateTime? DirectDebitMandateDate { get; private set; }
    }
}
