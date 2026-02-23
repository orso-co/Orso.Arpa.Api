using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.FinanceDomain.Enums;

namespace Orso.Arpa.Domain.FinanceDomain.Model
{
    public class PendingTanRequest : BaseEntity
    {
        public PendingTanRequest(
            Guid? id,
            Guid organizationBankAccountId,
            string tanChallenge,
            string tanMediumName,
            DateTime expiresAt) : base(id)
        {
            OrganizationBankAccountId = organizationBankAccountId;
            TanChallenge = tanChallenge;
            TanMediumName = tanMediumName;
            Status = TanRequestStatus.Pending;
            ExpiresAt = expiresAt;
        }

        [JsonConstructor]
        protected PendingTanRequest()
        {
        }

        public void Submit(string tan)
        {
            Status = TanRequestStatus.Submitted;
            SubmittedTan = tan;
        }

        public void Complete()
        {
            Status = TanRequestStatus.Completed;
        }

        public void Expire()
        {
            Status = TanRequestStatus.Expired;
        }

        public Guid OrganizationBankAccountId { get; private set; }
        public virtual OrganizationBankAccount OrganizationBankAccount { get; private set; }

        public string TanChallenge { get; private set; }
        public string TanMediumName { get; private set; }
        public TanRequestStatus Status { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public string SubmittedTan { get; private set; }
    }
}
