using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.MembershipImportApplication.Model
{
    public class MembershipImportPreviewDto
    {
        public List<MembershipImportRowDto> Rows { get; set; } = new();
        public int TotalRows { get; set; }
        public int MatchedCount { get; set; }
        public int AmbiguousCount { get; set; }
        public int NotFoundCount { get; set; }
    }

    public class MembershipImportRowDto
    {
        public int RowNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string MatchStatus { get; set; } // matched, ambiguous, not_found
        public Guid? MatchedPersonId { get; set; }
        public string MatchedPersonName { get; set; }
        public List<PersonMatchCandidateDto> Candidates { get; set; } = new();

        // Imported data
        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public decimal? AnnualFee { get; set; }
        public decimal? AnnualFee2025 { get; set; }
        public decimal? AnnualFee2024 { get; set; }
        public bool IsReduced { get; set; }
        public string MembershipType { get; set; }
        public string SupportLevel { get; set; }
        public string Iban { get; set; }
        public string MandateReference { get; set; }
        public DateTime? MandateDate { get; set; }
        public string Remarks { get; set; }
        public string ChoirOrchestra { get; set; }
    }

    public class PersonMatchCandidateDto
    {
        public Guid PersonId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string MatchReason { get; set; }
    }

    public class MembershipImportExecuteDto
    {
        public Guid ClubId { get; set; }
        public List<MembershipImportRowConfirmDto> Rows { get; set; } = new();
    }

    public class MembershipImportRowConfirmDto
    {
        public int RowNumber { get; set; }
        public Guid? PersonId { get; set; }
        public bool Import { get; set; }

        // For creating new persons (when PersonId is null)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Data to import
        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public decimal AnnualFee { get; set; }
        public decimal? AnnualFee2025 { get; set; }
        public decimal? AnnualFee2024 { get; set; }
        public bool IsReduced { get; set; }
        public Guid? SupportLevelId { get; set; }
        public Guid? MembershipStatusId { get; set; }
        public string Iban { get; set; }
        public string MandateReference { get; set; }
        public DateTime? MandateDate { get; set; }
        public string StaffComment { get; set; }
    }

    public class MembershipImportResultDto
    {
        public Guid ImportBatchId { get; set; }
        public int PersonsCreated { get; set; }
        public int MembershipsCreated { get; set; }
        public int BankAccountsCreated { get; set; }
        public int HistoryEntriesCreated { get; set; }
        public int Skipped { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class MembershipImportRollbackResultDto
    {
        public int PersonsDeleted { get; set; }
        public int MembershipsDeleted { get; set; }
        public int HistoryEntriesDeleted { get; set; }
        public int BankAccountsDeleted { get; set; }
        public int ContactDetailsDeleted { get; set; }
    }
}
