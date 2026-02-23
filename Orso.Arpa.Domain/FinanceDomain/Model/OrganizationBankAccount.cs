using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.FinanceDomain.Enums;

namespace Orso.Arpa.Domain.FinanceDomain.Model
{
    public class OrganizationBankAccount : BaseEntity
    {
        public OrganizationBankAccount(
            Guid? id,
            string name,
            string iban,
            string bic,
            string bankName,
            OrganizationAccountType accountType) : base(id)
        {
            Name = name;
            Iban = iban;
            Bic = bic;
            BankName = bankName;
            AccountType = accountType;
            IsActive = true;
        }

        [JsonConstructor]
        protected OrganizationBankAccount()
        {
        }

        public void Update(
            string name,
            string iban,
            string bic,
            string bankName,
            OrganizationAccountType accountType,
            bool isActive)
        {
            Name = name;
            Iban = iban;
            Bic = bic;
            BankName = bankName;
            AccountType = accountType;
            IsActive = isActive;
        }

        public void SetEncryptedFinTsCredentials(string encryptedCredentials)
        {
            EncryptedFinTsCredentials = encryptedCredentials;
        }

        public void SetEncryptedPayPalCredentials(string encryptedCredentials)
        {
            EncryptedPayPalCredentials = encryptedCredentials;
        }

        public string Name { get; private set; }
        public string Iban { get; private set; }
        public string Bic { get; private set; }
        public string BankName { get; private set; }
        public OrganizationAccountType AccountType { get; private set; }
        public bool IsActive { get; private set; }

        public string EncryptedFinTsCredentials { get; private set; }
        public string EncryptedPayPalCredentials { get; private set; }

        [CascadingSoftDelete]
        public virtual ICollection<BankAccountBalanceSnapshot> BalanceSnapshots { get; private set; } = new HashSet<BankAccountBalanceSnapshot>();

        [CascadingSoftDelete]
        public virtual ICollection<PendingTanRequest> TanRequests { get; private set; } = new HashSet<PendingTanRequest>();
    }
}
