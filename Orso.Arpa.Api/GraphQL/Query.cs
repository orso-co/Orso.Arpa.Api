using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Persistence.GraphQL;

namespace Orso.Arpa.Api.GraphQL
{
    public class Query
    {
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<MusicianProfile>> GetMusicianProfiles(GraphQLContext graphQLContext) =>
            new(graphQLContext.MusicianProfiles.ToListAsync());

        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Person>> GetPersons(GraphQLContext graphQLContext) =>
            new(graphQLContext.Persons.ToListAsync());

        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Project>> GetProjects(GraphQLContext graphQLContext) =>
            new(graphQLContext.Projects.ToListAsync());

        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<AuditLog>> GetAuditLogs(GraphQLContext graphQLContext) =>
            new(graphQLContext.AuditLogs.ToListAsync());

        /// <summary>
        /// Global search across Persons, Appointments, Projects, and News.
        /// Supports partial matches and fuzzy search using PostgreSQL pg_trgm.
        /// </summary>
        /// <param name="arpaContext">Database context (injected)</param>
        /// <param name="query">Search query (minimum 2 characters)</param>
        /// <param name="maxResults">Maximum number of results (default: 50)</param>
        /// <returns>List of search results ordered by relevance</returns>
        public Task<List<GlobalSearchResult>> GlobalSearch(
            [Service] IArpaContext arpaContext,
            string query,
            int maxResults = 50) =>
            arpaContext.GlobalSearchAsync(query, maxResults);
    }
}
