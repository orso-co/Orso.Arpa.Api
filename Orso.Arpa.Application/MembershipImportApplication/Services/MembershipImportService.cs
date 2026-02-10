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

namespace Orso.Arpa.Application.MembershipImportApplication.Services
{
    public interface IMembershipImportService
    {
        Task<MembershipImportPreviewDto> PreviewAsync(Stream csvStream, CancellationToken cancellationToken);
        Task<MembershipImportResultDto> ExecuteAsync(MembershipImportExecuteDto executeDto, CancellationToken cancellationToken);
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
            var result = new MembershipImportResultDto();
            var rowsToImport = executeDto.Rows.Where(r => r.Import).ToList();

            foreach (var row in rowsToImport)
            {
                try
                {
                    // Create PersonMembership directly
                    var membership = new PersonMembership(Guid.NewGuid(), new CreatePersonMembership.Command
                    {
                        PersonId = row.PersonId,
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
                    _arpaContext.Set<PersonMembership>().Add(membership);
                    result.MembershipsCreated++;

                    // Create BankAccount if IBAN provided
                    if (!string.IsNullOrWhiteSpace(row.Iban))
                    {
                        // Check if person already has this IBAN
                        var existingAccount = await _arpaContext.Set<BankAccount>()
                            .AnyAsync(ba => ba.PersonId == row.PersonId
                                && ba.Iban == row.Iban.Replace(" ", "")
                                && !ba.Deleted, cancellationToken);

                        if (!existingAccount)
                        {
                            var bankAccount = new BankAccount(Guid.NewGuid(), new CreateBankAccount.Command
                            {
                                PersonId = row.PersonId,
                                Iban = row.Iban.Replace(" ", ""),
                            });
                            _arpaContext.Set<BankAccount>().Add(bankAccount);
                            result.BankAccountsCreated++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Row {row.RowNumber}: {ex.Message}");
                    result.Skipped++;
                }
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

            // Read header line
            var headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                return rows;
            }

            var headers = ParseCsvLine(headerLine);
            var headerMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < headers.Length; i++)
            {
                headerMap[headers[i].Trim()] = i;
            }

            int rowNumber = 1;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                rowNumber++;
                var values = ParseCsvLine(line);
                var row = new CsvRow
                {
                    RowNumber = rowNumber,
                    LastName = GetValue(values, headerMap, "Nachname", "Familienname", "Last Name"),
                    FirstName = GetValue(values, headerMap, "Vorname", "First Name"),
                    Email = GetValue(values, headerMap, "E-Mail", "Email", "Mail"),
                    MembershipType = GetValue(values, headerMap, "Mitgliedstyp", "Typ", "Type", "Membership Type"),
                    SupportLevel = GetValue(values, headerMap, "Förderstufe", "Support Level"),
                    Iban = GetValue(values, headerMap, "IBAN"),
                    MandateReference = GetValue(values, headerMap, "Mandatsreferenz", "Mandate Reference", "Mandat"),
                    Remarks = GetValue(values, headerMap, "Hinweise", "Bemerkungen", "Remarks", "Notizen"),
                    ChoirOrchestra = GetValue(values, headerMap, "Chor/Orchester", "Chor", "Orchester", "Choir/Orchestra"),
                };

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

                // Parse annual fee (German format: 1.234,56 or 1234,56)
                var feeStr = GetValue(values, headerMap, "Jahresbeitrag 2025", "Jahresbeitrag 2024", "Jahresbeitrag", "Annual Fee");
                if (!string.IsNullOrWhiteSpace(feeStr))
                {
                    row.AnnualFee = ParseGermanDecimal(feeStr);
                }

                rows.Add(row);
            }

            return rows;
        }

        private static string[] ParseCsvLine(string line)
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
                else if ((c == ';' || c == ',') && !inQuotes)
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
            // Try German format first: dd.MM.yyyy, then ISO
            if (DateTime.TryParseExact(dateStr.Trim(), new[] { "dd.MM.yyyy", "d.M.yyyy", "dd.MM.yy", "yyyy-MM-dd" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }
            if (DateTime.TryParse(dateStr.Trim(), CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None, out var date2))
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
