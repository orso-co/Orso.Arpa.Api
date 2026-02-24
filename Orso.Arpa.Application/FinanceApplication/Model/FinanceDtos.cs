using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.FinanceApplication.Model
{
    public class OrganizationAccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public bool HasFinTsCredentials { get; set; }
        public bool HasPayPalCredentials { get; set; }
        public Guid? ClubId { get; set; }
        public string ClubName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public class CreateOrganizationAccountDto
    {
        public string Name { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public Guid? ClubId { get; set; }
        public FinTsCredentialsDto FinTsCredentials { get; set; }
        public PayPalCredentialsDto PayPalCredentials { get; set; }
    }

    public class ModifyOrganizationAccountDto
    {
        public string Name { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public Guid? ClubId { get; set; }
        public FinTsCredentialsDto FinTsCredentials { get; set; }
        public PayPalCredentialsDto PayPalCredentials { get; set; }
    }

    public class FinTsCredentialsDto
    {
        public string BankUrl { get; set; }
        public int BankCode { get; set; }
        public string UserId { get; set; }
        public string Pin { get; set; }
        public string AccountNumber { get; set; }
        public string SubAccountFeature { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string TanMethod { get; set; }
        public string TanMediumName { get; set; }
    }

    public class PayPalCredentialsDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool UseSandbox { get; set; }
    }

    public class BalanceSnapshotDto
    {
        public Guid Id { get; set; }
        public Guid OrganizationBankAccountId { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public decimal? AvailableBalance { get; set; }
        public string Currency { get; set; }
        public DateTime BalanceDate { get; set; }
        public DateTime SyncedAt { get; set; }
        public string SyncStatus { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class BalanceSummaryDto
    {
        public decimal TotalBalance { get; set; }
        public string Currency { get; set; }
        public List<AccountBalanceDto> Accounts { get; set; } = new();
    }

    public class AccountBalanceDto
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal? AvailableBalance { get; set; }
        public string Currency { get; set; }
        public DateTime? LastSyncedAt { get; set; }
        public string SyncStatus { get; set; }
        public Guid? ClubId { get; set; }
        public string ClubName { get; set; }
    }

    public class PendingTanRequestDto
    {
        public Guid Id { get; set; }
        public Guid OrganizationBankAccountId { get; set; }
        public string AccountName { get; set; }
        public string TanChallenge { get; set; }
        public string TanMediumName { get; set; }
        public string Status { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class SubmitTanDto
    {
        public Guid TanRequestId { get; set; }
        public string Tan { get; set; }
    }

    public class FinanceClubAccessDto
    {
        public Guid UserId { get; set; }
        public string UserDisplayName { get; set; }
        public Guid ClubId { get; set; }
        public string ClubName { get; set; }
        public string GrantedBy { get; set; }
        public DateTime GrantedAt { get; set; }
    }

    public class GrantFinanceAccessDto
    {
        public Guid UserId { get; set; }
        public Guid ClubId { get; set; }
    }

    public class RevokeFinanceAccessDto
    {
        public Guid UserId { get; set; }
        public Guid ClubId { get; set; }
    }

    public class MyFinanceAccessDto
    {
        public bool HasAccess { get; set; }
        public List<Guid> AccessibleClubIds { get; set; } = new();
    }
}
