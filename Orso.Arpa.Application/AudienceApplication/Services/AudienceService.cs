using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.AudienceApplication.Interfaces;
using Orso.Arpa.Application.AudienceApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Enums;

namespace Orso.Arpa.Application.AudienceApplication.Services
{
    public class AudienceService : IAudienceService
    {
        private readonly IArpaContext _arpaContext;

        public AudienceService(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<AudienceSearchResultDto> SearchAsync(AudienceSearchDto dto)
        {
            IQueryable<Person> query = _arpaContext.Persons
                .Include(p => p.Addresses)
                .Include(p => p.MusicianProfiles).ThenInclude(mp => mp.Instrument)
                .Include(p => p.MusicianProfiles).ThenInclude(mp => mp.ProjectParticipations)
                .Include(p => p.User)
                .Include(p => p.ContactDetails)
                .Include(p => p.PersonMemberships).ThenInclude(pm => pm.MembershipStatus).ThenInclude(ms => ms.SelectValue)
                .Include(p => p.PersonMemberships).ThenInclude(pm => pm.SupportLevel).ThenInclude(sl => sl.SelectValue)
                .AsQueryable();

            // Name search
            if (!string.IsNullOrWhiteSpace(dto.SearchQuery))
            {
                string search = dto.SearchQuery.Trim().ToLower();
                query = query.Where(p =>
                    (p.GivenName != null && p.GivenName.ToLower().Contains(search)) ||
                    (p.Surname != null && p.Surname.ToLower().Contains(search)));
            }

            // Section/Instrument filter
            if (dto.SectionIds != null && dto.SectionIds.Count > 0)
            {
                query = query.Where(p =>
                    p.MusicianProfiles.Any(mp => dto.SectionIds.Contains(mp.InstrumentId)));
            }

            // Project filter with AND/OR logic
            if (dto.ProjectIds != null && dto.ProjectIds.Count > 0)
            {
                if (dto.ProjectFilterOperator?.Equals("AND", StringComparison.OrdinalIgnoreCase) == true)
                {
                    // AND: person must have participation in ALL selected projects
                    foreach (var projectId in dto.ProjectIds)
                    {
                        var pid = projectId;
                        query = query.Where(p =>
                            p.MusicianProfiles.Any(mp =>
                                mp.ProjectParticipations.Any(pp => pp.ProjectId == pid)));
                    }
                }
                else
                {
                    // OR: person must have participation in ANY selected project
                    query = query.Where(p =>
                        p.MusicianProfiles.Any(mp =>
                            mp.ProjectParticipations.Any(pp => dto.ProjectIds.Contains(pp.ProjectId))));
                }

                // Participation status filter (uses mapped enum fields, not [NotMapped] computed property)
                if (dto.ParticipationStatuses != null && dto.ParticipationStatuses.Count > 0)
                {
                    bool wantAcceptance = dto.ParticipationStatuses.Contains("Acceptance");
                    bool wantRefusal = dto.ParticipationStatuses.Contains("Refusal");
                    bool wantPending = dto.ParticipationStatuses.Contains("Pending");

                    query = query.Where(p =>
                        p.MusicianProfiles.Any(mp =>
                            mp.ProjectParticipations.Any(pp =>
                                dto.ProjectIds.Contains(pp.ProjectId) && (
                                    (wantAcceptance &&
                                        pp.ParticipationStatusInner == ProjectParticipationStatusInner.Acceptance &&
                                        pp.ParticipationStatusInternal == ProjectParticipationStatusInternal.Acceptance) ||
                                    (wantRefusal && (
                                        pp.ParticipationStatusInner == ProjectParticipationStatusInner.Refusal ||
                                        pp.ParticipationStatusInner == ProjectParticipationStatusInner.RehearsalsOnly ||
                                        pp.ParticipationStatusInternal == ProjectParticipationStatusInternal.Refusal)) ||
                                    (wantPending &&
                                        !(pp.ParticipationStatusInner == ProjectParticipationStatusInner.Acceptance &&
                                          pp.ParticipationStatusInternal == ProjectParticipationStatusInternal.Acceptance) &&
                                        !(pp.ParticipationStatusInner == ProjectParticipationStatusInner.Refusal ||
                                          pp.ParticipationStatusInner == ProjectParticipationStatusInner.RehearsalsOnly ||
                                          pp.ParticipationStatusInternal == ProjectParticipationStatusInternal.Refusal))
                                ))));
                }

                // Invitation status filter (parse to enum for EF Core translation)
                if (dto.InvitationStatuses != null && dto.InvitationStatuses.Count > 0)
                {
                    var invitationEnums = new List<ProjectInvitationStatus>();
                    foreach (var s in dto.InvitationStatuses)
                    {
                        if (Enum.TryParse<ProjectInvitationStatus>(s, out var status))
                            invitationEnums.Add(status);
                    }

                    if (invitationEnums.Count > 0)
                    {
                        query = query.Where(p =>
                            p.MusicianProfiles.Any(mp =>
                                mp.ProjectParticipations.Any(pp =>
                                    dto.ProjectIds.Contains(pp.ProjectId) &&
                                    pp.InvitationStatus.HasValue &&
                                    invitationEnums.Contains(pp.InvitationStatus.Value))));
                    }
                }
            }

            // City filter
            if (!string.IsNullOrWhiteSpace(dto.City))
            {
                string city = dto.City.Trim().ToLower();
                query = query.Where(p =>
                    p.Addresses.Any(a => a.City != null && a.City.ToLower().Contains(city)));
            }

            // Country filter
            if (!string.IsNullOrWhiteSpace(dto.Country))
            {
                string country = dto.Country.Trim().ToLower();
                query = query.Where(p =>
                    p.Addresses.Any(a => a.Country != null && a.Country.ToLower().Contains(country)));
            }

            // Account filter
            if (dto.HasAccount.HasValue)
            {
                if (dto.HasAccount.Value)
                    query = query.Where(p => p.User != null);
                else
                    query = query.Where(p => p.User == null);
            }

            // Membership filters
            if (dto.HasMembership.HasValue)
            {
                if (dto.HasMembership.Value)
                    query = query.Where(p => p.PersonMemberships.Any(pm => !pm.Deleted));
                else
                    query = query.Where(p => !p.PersonMemberships.Any(pm => !pm.Deleted));
            }

            if (dto.MembershipStatusIds != null && dto.MembershipStatusIds.Count > 0)
            {
                query = query.Where(p =>
                    p.PersonMemberships.Any(pm => !pm.Deleted &&
                        pm.MembershipStatusId.HasValue &&
                        dto.MembershipStatusIds.Contains(pm.MembershipStatusId.Value)));
            }

            if (dto.SupportLevelIds != null && dto.SupportLevelIds.Count > 0)
            {
                query = query.Where(p =>
                    p.PersonMemberships.Any(pm => !pm.Deleted &&
                        pm.SupportLevelId.HasValue &&
                        dto.SupportLevelIds.Contains(pm.SupportLevelId.Value)));
            }

            if (dto.MembershipActive.HasValue)
            {
                var now = DateTime.UtcNow;
                if (dto.MembershipActive.Value)
                    query = query.Where(p =>
                        p.PersonMemberships.Any(pm => !pm.Deleted &&
                            (!pm.ExitDate.HasValue || pm.ExitDate.Value > now)));
                else
                    query = query.Where(p =>
                        p.PersonMemberships.Any(pm => !pm.Deleted &&
                            pm.ExitDate.HasValue && pm.ExitDate.Value <= now));
            }

            // Score filters
            if (dto.ScoreFilter != null)
            {
                ApplyScoreFilters(ref query, dto.ScoreFilter);
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Sorting
            query = ApplySorting(query, dto.SortBy, dto.SortDescending);

            // Pagination
            List<Person> persons = await query
                .Skip(dto.Skip)
                .Take(dto.Take)
                .ToListAsync();

            // Map to DTOs
            var items = persons.Select(p => MapToDto(p, dto.ProjectIds)).ToList();

            return new AudienceSearchResultDto
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        private static void ApplyScoreFilters(ref IQueryable<Person> query, ScoreFilterDto sf)
        {
            if (sf.MinReliability.HasValue)
                query = query.Where(p => p.Reliability >= sf.MinReliability.Value);
            if (sf.MaxReliability.HasValue)
                query = query.Where(p => p.Reliability <= sf.MaxReliability.Value);

            if (sf.MinGeneralPreference.HasValue)
                query = query.Where(p => p.GeneralPreference >= sf.MinGeneralPreference.Value);
            if (sf.MaxGeneralPreference.HasValue)
                query = query.Where(p => p.GeneralPreference <= sf.MaxGeneralPreference.Value);

            if (sf.MinExperienceLevel.HasValue)
                query = query.Where(p => p.ExperienceLevel >= sf.MinExperienceLevel.Value);
            if (sf.MaxExperienceLevel.HasValue)
                query = query.Where(p => p.ExperienceLevel <= sf.MaxExperienceLevel.Value);

            if (sf.MinLevelAssessmentTeam.HasValue)
                query = query.Where(p =>
                    p.MusicianProfiles.Any(mp => mp.IsMainProfile && mp.LevelAssessmentTeam >= sf.MinLevelAssessmentTeam.Value));
            if (sf.MaxLevelAssessmentTeam.HasValue)
                query = query.Where(p =>
                    p.MusicianProfiles.Any(mp => mp.IsMainProfile && mp.LevelAssessmentTeam <= sf.MaxLevelAssessmentTeam.Value));

            if (sf.MinProfilePreferenceTeam.HasValue)
                query = query.Where(p =>
                    p.MusicianProfiles.Any(mp => mp.IsMainProfile && mp.ProfilePreferenceTeam >= sf.MinProfilePreferenceTeam.Value));
            if (sf.MaxProfilePreferenceTeam.HasValue)
                query = query.Where(p =>
                    p.MusicianProfiles.Any(mp => mp.IsMainProfile && mp.ProfilePreferenceTeam <= sf.MaxProfilePreferenceTeam.Value));
        }

        private static IQueryable<Person> ApplySorting(IQueryable<Person> query, string sortBy, bool descending)
        {
            return sortBy?.ToLower() switch
            {
                "givenname" => descending ? query.OrderByDescending(p => p.GivenName) : query.OrderBy(p => p.GivenName),
                "surname" => descending ? query.OrderByDescending(p => p.Surname) : query.OrderBy(p => p.Surname),
                "instrument" => descending
                    ? query.OrderByDescending(p => p.MusicianProfiles
                        .Where(mp => mp.IsMainProfile).Select(mp => mp.Instrument.Name).FirstOrDefault())
                    : query.OrderBy(p => p.MusicianProfiles
                        .Where(mp => mp.IsMainProfile).Select(mp => mp.Instrument.Name).FirstOrDefault()),
                "city" => descending
                    ? query.OrderByDescending(p => p.Addresses.OrderBy(a => a.Id).Select(a => a.City).FirstOrDefault())
                    : query.OrderBy(p => p.Addresses.OrderBy(a => a.Id).Select(a => a.City).FirstOrDefault()),
                "hasaccount" => descending
                    ? query.OrderByDescending(p => p.User != null)
                    : query.OrderBy(p => p.User != null),
                "reliability" => descending ? query.OrderByDescending(p => p.Reliability) : query.OrderBy(p => p.Reliability),
                "generalpreference" => descending ? query.OrderByDescending(p => p.GeneralPreference) : query.OrderBy(p => p.GeneralPreference),
                "experiencelevel" => descending ? query.OrderByDescending(p => p.ExperienceLevel) : query.OrderBy(p => p.ExperienceLevel),
                _ => query.OrderBy(p => p.Surname).ThenBy(p => p.GivenName),
            };
        }

        private static AudiencePersonDto MapToDto(Person p, List<Guid> projectIds)
        {
            var mainProfile = p.MusicianProfiles.FirstOrDefault(mp => mp.IsMainProfile)
                ?? p.MusicianProfiles.FirstOrDefault();

            var projectParticipations = new List<ProjectParticipationInfoDto>();
            if (projectIds != null && projectIds.Count > 0)
            {
                var allParticipations = p.MusicianProfiles
                    .SelectMany(mp => mp.ProjectParticipations)
                    .Where(pp => projectIds.Contains(pp.ProjectId))
                    .ToList();

                projectParticipations = allParticipations
                    .Select(pp => new ProjectParticipationInfoDto
                    {
                        ProjectId = pp.ProjectId,
                        ParticipationStatus = pp.ParticipationStatusResult.ToString(),
                        InvitationStatus = pp.InvitationStatus?.ToString()
                    })
                    .ToList();
            }

            var activeMembership = p.PersonMemberships
                .Where(pm => !pm.Deleted)
                .OrderByDescending(pm => pm.EntryDate)
                .FirstOrDefault();

            return new AudiencePersonDto
            {
                Id = p.Id,
                DisplayName = p.DisplayName,
                GivenName = p.GivenName,
                Surname = p.Surname,
                City = p.Addresses.FirstOrDefault()?.City,
                MainInstrumentId = mainProfile?.InstrumentId,
                MainInstrument = mainProfile?.Instrument?.Name,
                AllInstrumentIds = p.MusicianProfiles
                    .Where(mp => mp.Instrument != null)
                    .Select(mp => mp.InstrumentId)
                    .Distinct()
                    .ToList(),
                AllInstruments = p.MusicianProfiles
                    .Where(mp => mp.Instrument != null)
                    .Select(mp => mp.Instrument.Name)
                    .Distinct()
                    .ToList(),
                HasAccount = p.User != null,
                Email = p.User?.Email ?? p.ContactDetails
                    .Where(cd => cd.Key == ContactDetailKey.EMail)
                    .OrderByDescending(cd => cd.Preference)
                    .FirstOrDefault()?.Value,
                Reliability = p.Reliability,
                GeneralPreference = p.GeneralPreference,
                ExperienceLevel = p.ExperienceLevel,
                LevelAssessmentTeam = mainProfile?.LevelAssessmentTeam ?? 0,
                ProfilePreferenceTeam = mainProfile?.ProfilePreferenceTeam ?? 0,
                ProjectParticipations = projectParticipations,
                MembershipStatus = activeMembership?.MembershipStatus?.SelectValue?.Name,
                SupportLevel = activeMembership?.SupportLevel?.SelectValue?.Name,
                MembershipActive = activeMembership != null
                    ? (!activeMembership.ExitDate.HasValue || activeMembership.ExitDate.Value > DateTime.UtcNow)
                    : null
            };
        }
    }
}
