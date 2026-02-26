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
        public bool IsSpecialCase { get; set; }
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
        public Guid? PaymentMethodId { get; set; }
        public Guid? PaymentFrequencyId { get; set; }
        public bool IsSpecialCase { get; set; }
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

    public class CsvFormatInfoDto
    {
        public string Description { get; set; }
        public string Delimiter { get; set; }
        public string Encoding { get; set; }
        public List<CsvColumnInfoDto> Columns { get; set; } = new();
        public string ExampleRow { get; set; }

        public static CsvFormatInfoDto Create() => new()
        {
            Description = "CSV-Datei mit Mitgliedsdaten. Trennzeichen wird automatisch erkannt (Komma oder Semikolon). Die erste Zeile muss die Spaltenüberschriften enthalten.",
            Delimiter = "Automatisch (Komma oder Semikolon)",
            Encoding = "UTF-8 (mit oder ohne BOM)",
            Columns = new List<CsvColumnInfoDto>
            {
                new() { Name = "Nachname", Aliases = "Familienname, Last Name", Required = true, Description = "Nachname der Person (max. 200 Zeichen)" },
                new() { Name = "Vorname", Aliases = "First Name", Required = true, Description = "Vorname der Person (max. 200 Zeichen)" },
                new() { Name = "Email", Aliases = "E-Mail, Mail", Required = false, Description = "E-Mail-Adresse. Wird zum Abgleich mit bestehenden Personen verwendet." },
                new() { Name = "Eintritt", Aliases = "Eintrittsdatum, Entry Date", Required = false, Description = "Eintrittsdatum. Formate: dd.MM.yyyy, yyyy-MM-dd oder nur Jahreszahl (z.B. 2023)" },
                new() { Name = "Austritt", Aliases = "Austrittsdatum, Exit Date", Required = false, Description = "Austrittsdatum (gleiche Formate wie Eintritt)" },
                new() { Name = "Mitgliedstyp", Aliases = "Mitgliedstypus, Typ, Status, Type", Required = false, Description = "Wird automatisch zugeordnet: Vollmitglied, Projektmitglied, Fördermitglied" },
                new() { Name = "Förderstufe", Aliases = "Support Level", Required = false, Description = "Wird automatisch zugeordnet: Sonata, Concerto, Symphony, Opera" },
                new() { Name = "Beitrag", Aliases = "Jahresbeitrag, Annual Fee", Required = false, Description = "Aktueller Jahresbeitrag. Deutsches Format: 120,00 oder 1.200,00" },
                new() { Name = "Beitrag 2025", Aliases = "Jahresbeitrag 2025", Required = false, Description = "Jahresbeitrag 2025 (erzeugt History-Eintrag)" },
                new() { Name = "Beitrag 2024", Aliases = "Jahresbeitrag 2024", Required = false, Description = "Jahresbeitrag 2024 (erzeugt History-Eintrag)" },
                new() { Name = "Ermäßigt", Aliases = "Ermässigt, Ermäßigung", Required = false, Description = "WAHR oder FALSCH" },
                new() { Name = "IBAN", Aliases = "Iban", Required = false, Description = "Bankverbindung (max. 34 Zeichen, Leerzeichen werden entfernt)" },
                new() { Name = "Mandatsreferenz", Aliases = "Mandate Reference, Mandat", Required = false, Description = "SEPA-Mandatsreferenz (max. 100 Zeichen)" },
                new() { Name = "Mandatsdatum", Aliases = "Mandate Date", Required = false, Description = "Datum des SEPA-Mandats (gleiche Formate wie Eintritt)" },
                new() { Name = "Hinweise", Aliases = "Bemerkungen, Remarks, Notizen", Required = false, Description = "Freitext-Hinweise" },
                new() { Name = "Chor - aktiv", Aliases = "Chor -aktiv", Required = false, Description = "WAHR/FALSCH — Aktives Chormitglied" },
                new() { Name = "Chor - inaktiv", Aliases = "Chor -inaktiv", Required = false, Description = "WAHR/FALSCH — Inaktives Chormitglied" },
                new() { Name = "Orchester", Aliases = "Orchestra, Chor/Orchester", Required = false, Description = "WAHR/FALSCH — Orchestermitglied" },
            },
            ExampleRow = "Nachname;Vorname;Email;Eintritt;Mitgliedstyp;Förderstufe;Beitrag;IBAN;Mandatsreferenz\nMustermann;Max;max@example.com;01.01.2023;Vollmitglied;Concerto;120,00;DE89370400440532013000;MANDATE-001",
        };
    }

    public class CsvColumnInfoDto
    {
        public string Name { get; set; }
        public string Aliases { get; set; }
        public bool Required { get; set; }
        public string Description { get; set; }
    }

    public class CsvHeadersResponseDto
    {
        public List<string> Headers { get; set; } = new();
        public Dictionary<string, string> SuggestedMapping { get; set; } = new();
    }

    public class MembershipImportPreviewRequestDto
    {
        public Dictionary<string, string> ColumnMapping { get; set; }
    }
}
