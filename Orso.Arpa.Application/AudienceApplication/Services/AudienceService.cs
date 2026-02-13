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

            // Project filter with participation/invitation status
            if (dto.ProjectId.HasValue)
            {
                query = query.Where(p =>
                    p.MusicianProfiles.Any(mp =>
                        mp.ProjectParticipations.Any(pp => pp.ProjectId == dto.ProjectId.Value)));

                if (dto.ParticipationStatuses != null && dto.ParticipationStatuses.Count > 0)
                {
                    query = query.Where(p =>
                        p.MusicianProfiles.Any(mp =>
                            mp.ProjectParticipations.Any(pp =>
                                pp.ProjectId == dto.ProjectId.Value &&
                                dto.ParticipationStatuses.Contains(pp.ParticipationStatusResult.ToString()))));
                }

                if (dto.InvitationStatuses != null && dto.InvitationStatuses.Count > 0)
                {
                    query = query.Where(p =>
                        p.MusicianProfiles.Any(mp =>
                            mp.ProjectParticipations.Any(pp =>
                                pp.ProjectId == dto.ProjectId.Value &&
                                pp.InvitationStatus.HasValue &&
                                dto.InvitationStatuses.Contains(pp.InvitationStatus.Value.ToString()))));
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
            var items = persons.Select(p => MapToDto(p, dto.ProjectId)).ToList();

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
                "reliability" => descending ? query.OrderByDescending(p => p.Reliability) : query.OrderBy(p => p.Reliability),
                "generalpreference" => descending ? query.OrderByDescending(p => p.GeneralPreference) : query.OrderBy(p => p.GeneralPreference),
                "experiencelevel" => descending ? query.OrderByDescending(p => p.ExperienceLevel) : query.OrderBy(p => p.ExperienceLevel),
                _ => query.OrderBy(p => p.Surname).ThenBy(p => p.GivenName),
            };
        }

        private static AudiencePersonDto MapToDto(Person p, Guid? projectId)
        {
            var mainProfile = p.MusicianProfiles.FirstOrDefault(mp => mp.IsMainProfile)
                ?? p.MusicianProfiles.FirstOrDefault();

            string participationStatus = null;
            string invitationStatus = null;
            if (projectId.HasValue && mainProfile != null)
            {
                var participation = mainProfile.ProjectParticipations
                    .FirstOrDefault(pp => pp.ProjectId == projectId.Value);
                if (participation != null)
                {
                    participationStatus = participation.ParticipationStatusResult.ToString();
                    invitationStatus = participation.InvitationStatus?.ToString();
                }
            }

            return new AudiencePersonDto
            {
                Id = p.Id,
                DisplayName = p.DisplayName,
                GivenName = p.GivenName,
                Surname = p.Surname,
                City = p.Addresses.FirstOrDefault()?.City,
                MainInstrument = mainProfile?.Instrument?.Name,
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
                ParticipationStatus = participationStatus,
                InvitationStatus = invitationStatus
            };
        }
    }
}
