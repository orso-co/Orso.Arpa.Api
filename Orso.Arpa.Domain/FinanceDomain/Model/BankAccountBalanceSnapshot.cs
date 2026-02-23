using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.FinanceDomain.Enums;

namespace Orso.Arpa.Domain.FinanceDomain.Model
{
    public class BankAccountBalanceSnapshot : BaseEntity
    {
        public BankAccountBalanceSnapshot(
            Guid? id,
            Guid organizationBankAccountId,
            decimal balance,
            decimal? availableBalance,
            string currency,
            DateTime balanceDate,
            BalanceSyncStatus syncStatus,
            string errorMessage = null) : base(id)
        {
            OrganizationBankAccountId = organizationBankAccountId;
            Balance = balance;
            AvailableBalance = availableBalance;
            Currency = currency;
            BalanceDate = balanceDate;
            SyncedAt = DateTime.UtcNow;
            SyncStatus = syncStatus;
            ErrorMessage = errorMessage;
        }

        [JsonConstructor]
        protected BankAccountBalanceSnapshot()
        {
        }

        public Guid OrganizationBankAccountId { get; private set; }
        public virtual OrganizationBankAccount OrganizationBankAccount { get; private set; }

        public decimal Balance { get; private set; }
        public decimal? AvailableBalance { get; private set; }
        public string Currency { get; private set; }
        public DateTime BalanceDate { get; private set; }
        public DateTime SyncedAt { get; private set; }
        public BalanceSyncStatus SyncStatus { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}
