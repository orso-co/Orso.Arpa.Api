using System;
using Orso.Arpa.Domain._General.Interfaces;
using Orso.Arpa.Domain._General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.ClubDomain.Model
{
    public class ClubMembershipProfile : BaseEntity, IVersionedEntity
    {
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid MembershipSubTypeId { get; private set; }
        public virtual ClubMembershipSubType MembershipSubType { get; private set; }
        public decimal? DeviatingAnnualContributionInEuro { get; private set; }
        public string ReasonForDeviatingAnnualContribution { get; private set; }
        public int? DeviatingTerminationPeriodInMonths { get; private set; }
        public string ReasonForDeviatingTerminationPeriod { get; private set; }
        public DateTime ValidFrom { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public bool IsDirectDebitMandateGranted { get; private set; }
        public DateTime? MembershipTerminationDate { get; private set; }
        public string TerminationReason { get; private set; }
        public DateTime JoiningDate { get; private set; }
    }
}