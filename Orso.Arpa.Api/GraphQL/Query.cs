using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.AuditLogDomain.Model;
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
    }
}
