using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.MembershipImportApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Application.MembershipImportApplication.Services
{
    public interface IMembershipImportService
    {
        Task<MembershipImportPreviewDto> PreviewAsync(Stream csvStream, CancellationToken cancellationToken);
        Task<MembershipImportResultDto> ExecuteAsync(MembershipImportExecuteDto executeDto, CancellationToken cancellationToken);
        Task<MembershipImportRollbackResultDto> RollbackAsync(Guid importBatchId, CancellationToken cancellationToken);
    }

    public class MembershipImportService : IMembershipImportService
    {
        private readonly IArpaContext _arpaContext;

        public MembershipImportService(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<MembershipImportPreviewDto> PreviewAsync(Stream csvStream, CancellationToken cancellationToken)
        {
            var rows = ParseCsv(csvStream);
            var result = new MembershipImportPreviewDto
            {
                TotalRows = rows.Count,
            };

            // Load all persons with their contact details for matching
            var persons = await _arpaContext.Persons
                .Where(p => !p.Deleted)
                .Include(p => p.ContactDetails.Where(cd => cd.Key == ContactDetailKey.EMail && !cd.Deleted))
                .Include(p => p.User)
                .ToListAsync(cancellationToken);

            foreach (var row in rows)
            {
                var importRow = new MembershipImportRowDto
                {
                    RowNumber = row.RowNumber,
                    LastName = row.LastName,
                    FirstName = row.FirstName,
                    Email = row.Email,
                    EntryDate = row.EntryDate,
                    ExitDate = row.ExitDate,
                    AnnualFee = row.AnnualFee,
                    AnnualFee2025 = row.Fee2025,
                    AnnualFee2024 = row.Fee2024,
                    IsReduced = row.IsReduced,
                    MembershipType = row.MembershipType,
                    SupportLevel = row.SupportLevel,
                    Iban = row.Iban,
                    MandateReference = row.MandateReference,
                    MandateDate = row.MandateDate,
                    Remarks = row.Remarks,
                    ChoirOrchestra = row.ChoirOrchestra,
                };

                MatchPerson(importRow, persons);
                result.Rows.Add(importRow);
            }

            result.MatchedCount = result.Rows.Count(r => r.MatchStatus == "matched");
            result.AmbiguousCount = result.Rows.Count(r => r.MatchStatus == "ambiguous");
            result.NotFoundCount = result.Rows.Count(r => r.MatchStatus == "not_found");

            return result;
        }

        public async Task<MembershipImportResultDto> ExecuteAsync(MembershipImportExecuteDto executeDto, CancellationToken cancellationToken)
        {
            var batchId = Guid.NewGuid();
            var result = new MembershipImportResultDto { ImportBatchId = batchId };
            var rowsToImport = executeDto.Rows.Where(r => r.Import).ToList();

            Console.WriteLine($"[CSV-Import Execute] BatchId: {batchId}, ClubId: {executeDto.ClubId}, Rows to import: {rowsToImport.Count}");

            // Lookup "Diverse" gender as default for new persons
            var diverseGenderId = await _arpaContext.Set<SelectValueMapping>()
                .Where(m => !m.Deleted
                    && m.SelectValueCategory.Table == "Person"
                    && m.SelectValueCategory.Property == "Gender"
                    && m.SelectValue.Name == "Diverse")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (diverseGenderId == Guid.Empty)
            {
                Console.WriteLine("[CSV-Import Execute] WARNING: 'Diverse' gender not found, using Guid.Empty");
            }

            foreach (var row in rowsToImport)
            {
                try
                {
                    var personId = row.PersonId ?? Guid.Empty;

                    // Create new person if not matched to an existing one
                    if (personId == Guid.Empty)
                    {
                        var person = new Person(Guid.NewGuid(), new CreatePerson.Command
                        {
                            GivenName = row.FirstName,
                            Surname = row.LastName,
                            GenderId = diverseGenderId,
                        });
                        person.ImportBatchId = batchId;
                        _arpaContext.Set<Person>().Add(person);
                        personId = person.Id;
                        result.PersonsCreated++;

                        // Add email as contact detail
                        if (!string.IsNullOrWhiteSpace(row.Email))
                        {
                            var contactDetail = new ContactDetail(Guid.NewGuid(), new CreateContactDetail.Command
                            {
                                PersonId = person.Id,
                                Key = ContactDetailKey.EMail,
                                Value = row.Email.Trim(),
                                Preference = 1,
                            });
                            _arpaContext.Set<ContactDetail>().Add(contactDetail);
                        }
                    }

                    // Create PersonMembership directly
                    var membership = new PersonMembership(Guid.NewGuid(), new CreatePersonMembership.Command
                    {
                        PersonId = personId,
                        EntryDate = row.EntryDate ?? DateTime.UtcNow,
                        ExitDate = row.ExitDate,
                        AnnualFee = row.AnnualFee,
                        SupportLevelId = row.SupportLevelId,
                        MembershipStatusId = row.MembershipStatusId,
                        ClubId = executeDto.ClubId,
                        MandateReference = row.MandateReference,
                        MandateDate = row.MandateDate,
                        StaffComment = row.StaffComment,
                    });
                    membership.ImportBatchId = batchId;
                    _arpaContext.Set<PersonMembership>().Add(membership);
                    result.MembershipsCreated++;

                    // Create MembershipHistory entries for each year with a fee
                    if (row.AnnualFee2025.HasValue && row.AnnualFee2025.Value > 0)
                    {
                        var history2025 = new MembershipHistory(Guid.NewGuid(), new CreateMembershipHistory.Command
                        {
                            PersonMembershipId = membership.Id,
                            Year = 2025,
                            Amount = row.AnnualFee2025.Value,
                            IsReduced = row.IsReduced,
                            Comment = row.StaffComment,
                        });
                        _arpaContext.Set<MembershipHistory>().Add(history2025);
                        result.HistoryEntriesCreated++;
                    }

                    if (row.AnnualFee2024.HasValue && row.AnnualFee2024.Value > 0)
                    {
                        var history2024 = new MembershipHistory(Guid.NewGuid(), new CreateMembershipHistory.Command
                        {
                            PersonMembershipId = membership.Id,
                            Year = 2024,
                            Amount = row.AnnualFee2024.Value,
                            IsReduced = row.IsReduced,
                            Comment = row.StaffComment,
                        });
                        _arpaContext.Set<MembershipHistory>().Add(history2024);
                        result.HistoryEntriesCreated++;
                    }

                    // Create BankAccount if IBAN provided
                    if (!string.IsNullOrWhiteSpace(row.Iban))
                    {
                        // Check if person already has this IBAN
                        var existingAccount = await _arpaContext.Set<BankAccount>()
                            .AnyAsync(ba => ba.PersonId == personId
                                && ba.Iban == row.Iban.Replace(" ", "")
                                && !ba.Deleted, cancellationToken);

                        if (!existingAccount)
                        {
                            var bankAccount = new BankAccount(Guid.NewGuid(), new CreateBankAccount.Command
                            {
                                PersonId = personId,
                                Iban = row.Iban.Replace(" ", ""),
                            });
                            _arpaContext.Set<BankAccount>().Add(bankAccount);
                            result.BankAccountsCreated++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CSV-Import Execute] Row {row.RowNumber} error: {ex.Message}");
                    result.Errors.Add($"Row {row.RowNumber}: {ex.Message}");
                    result.Skipped++;
                }
            }

            Console.WriteLine($"[CSV-Import Execute] Saving: {result.PersonsCreated} persons, {result.MembershipsCreated} memberships, {result.HistoryEntriesCreated} history, {result.BankAccountsCreated} bank accounts, {result.Skipped} skipped");

            try
            {
                await _arpaContext.SaveChangesAsync(cancellationToken);
                Console.WriteLine($"[CSV-Import Execute] SaveChangesAsync succeeded for batch {batchId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CSV-Import Execute] SaveChangesAsync FAILED: {ex.GetType().Name}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[CSV-Import Execute] Inner: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
                throw;
            }

            return result;
        }

        public async Task<MembershipImportRollbackResultDto> RollbackAsync(Guid importBatchId, CancellationToken cancellationToken)
        {
            var result = new MembershipImportRollbackResultDto();

            // Find all memberships from this batch
            var memberships = await _arpaContext.Set<PersonMembership>()
                .Where(m => m.ImportBatchId == importBatchId)
                .Include(m => m.MembershipHistories)
                .ToListAsync(cancellationToken);

            foreach (var membership in memberships)
            {
                // Hard-delete history entries
                foreach (var history in membership.MembershipHistories)
                {
                    _arpaContext.Set<MembershipHistory>().Remove(history);
                    result.HistoryEntriesDeleted++;
                }

                // Hard-delete bank accounts for this person (created during same import)
                var bankAccounts = await _arpaContext.Set<BankAccount>()
                    .Where(ba => ba.PersonId == membership.PersonId && !ba.Deleted)
                    .ToListAsync(cancellationToken);
                foreach (var ba in bankAccounts)
                {
                    _arpaContext.Set<BankAccount>().Remove(ba);
                    result.BankAccountsDeleted++;
                }

                // Hard-delete the membership
                _arpaContext.Set<PersonMembership>().Remove(membership);
                result.MembershipsDeleted++;
            }

            // Find and delete persons created by this batch
            var persons = await _arpaContext.Set<Person>()
                .Where(p => p.ImportBatchId == importBatchId)
                .ToListAsync(cancellationToken);

            foreach (var person in persons)
            {
                // Delete contact details
                var contactDetails = await _arpaContext.Set<ContactDetail>()
                    .Where(cd => cd.PersonId == person.Id)
                    .ToListAsync(cancellationToken);
                foreach (var cd in contactDetails)
                {
                    _arpaContext.Set<ContactDetail>().Remove(cd);
                    result.ContactDetailsDeleted++;
                }

                _arpaContext.Set<Person>().Remove(person);
                result.PersonsDeleted++;
            }

            await _arpaContext.SaveChangesAsync(cancellationToken);
            return result;
        }

        private void MatchPerson(MembershipImportRowDto row, List<Person> persons)
        {
            var candidates = new List<PersonMatchCandidateDto>();
            var csvEmail = row.Email?.Trim().ToLowerInvariant();
            var csvFirstName = row.FirstName?.Trim().ToLowerInvariant();
            var csvLastName = row.LastName?.Trim().ToLowerInvariant();

            foreach (var person in persons)
            {
                var personFirstName = person.GivenName?.Trim().ToLowerInvariant();
                var personLastName = person.Surname?.Trim().ToLowerInvariant();
                var personEmails = GetPersonEmails(person);

                bool nameMatch = !string.IsNullOrEmpty(csvFirstName)
                    && !string.IsNullOrEmpty(csvLastName)
                    && csvFirstName == personFirstName
                    && csvLastName == personLastName;

                bool emailMatch = !string.IsNullOrEmpty(csvEmail)
                    && personEmails.Any(e => e.Equals(csvEmail, StringComparison.OrdinalIgnoreCase));

                if (nameMatch && emailMatch)
                {
                    // Exact match: name + email
                    candidates.Add(new PersonMatchCandidateDto
                    {
                        PersonId = person.Id,
                        DisplayName = person.DisplayName,
                        Email = personEmails.FirstOrDefault(),
                        MatchReason = "Name + E-Mail stimmen überein",
                    });
                }
                else if (nameMatch)
                {
                    // Name match only
                    candidates.Add(new PersonMatchCandidateDto
                    {
                        PersonId = person.Id,
                        DisplayName = person.DisplayName,
                        Email = personEmails.FirstOrDefault(),
                        MatchReason = "Nur Name stimmt überein (E-Mail abweichend)",
                    });
                }
                else if (emailMatch)
                {
                    // Email match only
                    candidates.Add(new PersonMatchCandidateDto
                    {
                        PersonId = person.Id,
                        DisplayName = person.DisplayName,
                        Email = personEmails.FirstOrDefault(),
                        MatchReason = "Nur E-Mail stimmt überein (Name abweichend)",
                    });
                }
            }

            if (candidates.Count == 1 && candidates[0].MatchReason.Contains("Name + E-Mail"))
            {
                row.MatchStatus = "matched";
                row.MatchedPersonId = candidates[0].PersonId;
                row.MatchedPersonName = candidates[0].DisplayName;
            }
            else if (candidates.Count == 1 && candidates[0].MatchReason.Contains("Nur Name"))
            {
                // Single name match without email → auto-match (likely just missing/changed email)
                row.MatchStatus = "matched";
                row.MatchedPersonId = candidates[0].PersonId;
                row.MatchedPersonName = candidates[0].DisplayName;
                row.Candidates = candidates;
            }
            else if (candidates.Count > 0)
            {
                row.MatchStatus = "ambiguous";
                row.Candidates = candidates;
            }
            else
            {
                row.MatchStatus = "not_found";
            }
        }

        private static List<string> GetPersonEmails(Person person)
        {
            var emails = new List<string>();

            // User email
            if (!string.IsNullOrEmpty(person.User?.Email))
            {
                emails.Add(person.User.Email.ToLowerInvariant());
            }

            // Contact detail emails
            foreach (var cd in person.ContactDetails)
            {
                if (!string.IsNullOrEmpty(cd.Value))
                {
                    emails.Add(cd.Value.ToLowerInvariant());
                }
            }

            return emails.Distinct().ToList();
        }

        private static List<CsvRow> ParseCsv(Stream stream)
        {
            var rows = new List<CsvRow>();
            using var reader = new StreamReader(stream);

            // Read header line and strip BOM
            var headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                return rows;
            }
            headerLine = headerLine.TrimStart('\uFEFF');

            // Auto-detect delimiter: count semicolons vs commas in header (outside quotes)
            char delimiter = DetectDelimiter(headerLine);

            var headers = ParseCsvLine(headerLine, delimiter);
            var headerMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < headers.Length; i++)
            {
                headerMap[headers[i].Trim()] = i;
            }

            // Debug: Log headers
            Console.WriteLine($"[CSV-Import] Delimiter: '{delimiter}', Headers ({headers.Length}): {string.Join(" | ", headers.Select(h => $"'{h.Trim()}'"))}");

            int rowNumber = 1;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                rowNumber++;
                var values = ParseCsvLine(line, delimiter);
                var row = new CsvRow
                {
                    RowNumber = rowNumber,
                    LastName = GetValue(values, headerMap, "Nachname", "Familienname", "Last Name"),
                    FirstName = GetValue(values, headerMap, "Vorname", "First Name"),
                    Email = GetValue(values, headerMap, "E-Mail", "Email", "Mail"),
                    MembershipType = GetValue(values, headerMap, "Mitgliedstyp", "Mitgliedstypus", "Typ", "Type", "Membership Type", "Status"),
                    SupportLevel = GetValue(values, headerMap, "Förderstufe", "Support Level"),
                    Iban = GetValue(values, headerMap, "IBAN"),
                    MandateReference = GetValue(values, headerMap, "Mandatsreferenz", "Mandate Reference", "Mandat"),
                    Remarks = GetValue(values, headerMap, "Hinweise", "Bemerkungen", "Remarks", "Notizen"),
                };

                // Build ChoirOrchestra from separate columns (Numbers export)
                row.ChoirOrchestra = BuildChoirOrchestra(values, headerMap);

                // Parse Ermässigt flag
                var ermaessigt = GetValue(values, headerMap, "Ermässigt", "Ermäßigt", "Ermäßigung", "Ermässigung");
                row.IsReduced = ermaessigt?.Equals("WAHR", StringComparison.OrdinalIgnoreCase) == true;

                // Collect all unmapped columns as extra remarks
                var knownHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "Nachname", "Familienname", "Last Name",
                    "Vorname", "First Name",
                    "E-Mail", "Email", "Mail",
                    "Eintrittsdatum", "Eintritt", "Entry Date",
                    "Austritt", "Austrittsdatum", "Exit Date",
                    "Mitgliedstyp", "Mitgliedstypus", "Typ", "Type", "Membership Type", "Status",
                    "Förderstufe", "Förderstatus", "Support Level",
                    "Beitrag 2025", "Jahresbeitrag 2025", "Beitrag 2024", "Jahresbeitrag 2024",
                    "Jahresbeitrag", "Beitrag", "Annual Fee",
                    "Ermässigt", "Ermäßigt", "Ermäßigung", "Ermässigung",
                    "IBAN", "Iban",
                    "BIC", "Bic", "BIc",
                    "Mandatsreferenz", "Mandate Reference", "Mandat",
                    "Mandatsdatum", "Mandate Date",
                    "Hinweise", "Bemerkungen", "Remarks", "Notizen",
                    "Chor - aktiv", "Chor -aktiv", "Chor  - inaktiv", "Chor - inaktiv", "Chor -inaktiv",
                    "Orchester", "Orchestra", "Chor/Orchester",
                    "Anrede", "Geburtsjahr", "Geburtsdatum", "Alter",
                    "Telefon", "UUID", "UUID Person",
                };
                var extraParts = new List<string>();
                foreach (var kvp in headerMap)
                {
                    if (knownHeaders.Contains(kvp.Key)) continue;
                    if (kvp.Value < values.Length)
                    {
                        var val = values[kvp.Value].Trim();
                        if (!string.IsNullOrWhiteSpace(val) && !val.Equals("FALSCH", StringComparison.OrdinalIgnoreCase))
                        {
                            extraParts.Add(val.Equals("WAHR", StringComparison.OrdinalIgnoreCase)
                                ? kvp.Key
                                : $"{kvp.Key}: {val}");
                        }
                    }
                }
                if (extraParts.Count > 0)
                {
                    var extra = string.Join(" | ", extraParts);
                    row.Remarks = string.IsNullOrEmpty(row.Remarks) ? extra : $"{row.Remarks} | {extra}";
                }

                if (row.IsReduced)
                {
                    row.Remarks = string.IsNullOrEmpty(row.Remarks) ? "Ermäßigt" : $"{row.Remarks} | Ermäßigt";
                }

                // Parse dates (German format: dd.MM.yyyy)
                var entryDateStr = GetValue(values, headerMap, "Eintrittsdatum", "Eintritt", "Entry Date");
                if (!string.IsNullOrWhiteSpace(entryDateStr))
                {
                    row.EntryDate = ParseGermanDate(entryDateStr);
                }

                var exitDateStr = GetValue(values, headerMap, "Austritt", "Austrittsdatum", "Exit Date");
                if (!string.IsNullOrWhiteSpace(exitDateStr))
                {
                    row.ExitDate = ParseGermanDate(exitDateStr);
                }

                var mandateDateStr = GetValue(values, headerMap, "Mandatsdatum", "Mandate Date");
                if (!string.IsNullOrWhiteSpace(mandateDateStr))
                {
                    row.MandateDate = ParseGermanDate(mandateDateStr);
                }

                // Parse annual fees per year (German format: 1.234,56 or 1234,56)
                var fee2025Str = GetValue(values, headerMap, "Beitrag 2025", "Jahresbeitrag 2025");
                if (!string.IsNullOrWhiteSpace(fee2025Str))
                {
                    row.Fee2025 = ParseGermanDecimal(fee2025Str);
                }

                var fee2024Str = GetValue(values, headerMap, "Beitrag 2024", "Jahresbeitrag 2024");
                if (!string.IsNullOrWhiteSpace(fee2024Str))
                {
                    row.Fee2024 = ParseGermanDecimal(fee2024Str);
                }

                // Fallback: single "Jahresbeitrag" / "Beitrag" / "Annual Fee" column
                if (row.Fee2025 == null && row.Fee2024 == null)
                {
                    var feeStr = GetValue(values, headerMap, "Jahresbeitrag", "Beitrag", "Annual Fee");
                    if (!string.IsNullOrWhiteSpace(feeStr))
                    {
                        row.Fee2025 = ParseGermanDecimal(feeStr);
                    }
                }

                // AnnualFee on membership = most recent value (2025, fallback 2024)
                row.AnnualFee = row.Fee2025 ?? row.Fee2024;

                rows.Add(row);
            }

            return rows;
        }

        private static string BuildChoirOrchestra(string[] values, Dictionary<string, int> headerMap)
        {
            // Handle separate columns: "Chor - aktiv", "Chor  - inaktiv", "Orchester"
            var parts = new List<string>();

            var choirActive = GetValue(values, headerMap, "Chor - aktiv", "Chor -aktiv");
            var choirInactive = GetValue(values, headerMap, "Chor  - inaktiv", "Chor - inaktiv", "Chor -inaktiv");
            var orchestra = GetValue(values, headerMap, "Orchester", "Orchestra");

            if (choirActive?.Equals("WAHR", StringComparison.OrdinalIgnoreCase) == true)
            {
                parts.Add("Chor aktiv");
            }
            else if (choirInactive?.Equals("WAHR", StringComparison.OrdinalIgnoreCase) == true)
            {
                parts.Add("Chor inaktiv");
            }

            if (orchestra?.Equals("WAHR", StringComparison.OrdinalIgnoreCase) == true)
            {
                parts.Add("Orchester");
            }

            if (parts.Count > 0)
            {
                return string.Join(", ", parts);
            }

            // Fallback: single combined column
            return GetValue(values, headerMap, "Chor/Orchester");
        }

        private static char DetectDelimiter(string headerLine)
        {
            int semicolons = 0, commas = 0;
            bool inQuotes = false;

            foreach (char c in headerLine)
            {
                if (c == '"') inQuotes = !inQuotes;
                else if (!inQuotes)
                {
                    if (c == ';') semicolons++;
                    else if (c == ',') commas++;
                }
            }

            return semicolons >= commas ? ';' : ',';
        }

        private static string[] ParseCsvLine(string line, char delimiter)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var current = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++; // Skip escaped quote
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == delimiter && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            result.Add(current.ToString());
            return result.ToArray();
        }

        private static string GetValue(string[] values, Dictionary<string, int> headerMap, params string[] possibleHeaders)
        {
            foreach (var header in possibleHeaders)
            {
                if (headerMap.TryGetValue(header, out int index) && index < values.Length)
                {
                    var val = values[index].Trim();
                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        return val;
                    }
                }
            }
            return null;
        }

        private static DateTime? ParseGermanDate(string dateStr)
        {
            var s = dateStr.Trim();

            // Handle year-only: "2023", "2024"
            if (s.Length == 4 && int.TryParse(s, out int year) && year >= 1900 && year <= 2100)
            {
                return new DateTime(year, 1, 1);
            }

            // Try German format: dd.MM.yyyy, d.M.yyyy, dd.MM.yy
            if (DateTime.TryParseExact(s, new[] { "dd.MM.yyyy", "d.M.yyyy", "dd.MM.yy", "yyyy-MM-dd" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            // Try US format: MM/dd/yyyy, M/d/yyyy
            if (DateTime.TryParseExact(s, new[] { "MM/dd/yyyy", "M/d/yyyy", "M/dd/yyyy" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateUs))
            {
                return dateUs;
            }

            // Fallback: try German culture parse
            if (DateTime.TryParse(s, CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None, out var date2))
            {
                return date2;
            }
            return null;
        }

        private static decimal? ParseGermanDecimal(string str)
        {
            // Remove currency symbols and whitespace
            str = str.Trim().Replace("€", "").Replace(" ", "").Trim();
            if (string.IsNullOrEmpty(str) || str == "-")
            {
                return null;
            }

            // German format: 1.234,56 → remove dots, replace comma with period
            if (str.Contains(','))
            {
                str = str.Replace(".", "").Replace(",", ".");
            }

            if (decimal.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return null;
        }

        private class CsvRow
        {
            public int RowNumber { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Email { get; set; }
            public DateTime? EntryDate { get; set; }
            public DateTime? ExitDate { get; set; }
            public decimal? AnnualFee { get; set; }
            public decimal? Fee2025 { get; set; }
            public decimal? Fee2024 { get; set; }
            public bool IsReduced { get; set; }
            public string MembershipType { get; set; }
            public string SupportLevel { get; set; }
            public string Iban { get; set; }
            public string MandateReference { get; set; }
            public DateTime? MandateDate { get; set; }
            public string Remarks { get; set; }
            public string ChoirOrchestra { get; set; }
        }
    }
}
