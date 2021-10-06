using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.GraphQL;

namespace Orso.Arpa.Api.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<MusicianProfile>> GetMusicianProfiles([ScopedService] GraphQLContext context) =>
            context.MusicianProfiles.ToListAsync();

        [UseApplicationDbContext]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Person>> GetPersons([ScopedService] GraphQLContext context) =>
            context.Persons.ToListAsync();

        [UseApplicationDbContext]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Project>> GetProjects([ScopedService] GraphQLContext context) =>
            context.Projects.ToListAsync();

        [UseApplicationDbContext]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<AuditLog>> GetAuditLogs([ScopedService] GraphQLContext context) =>
            context.AuditLogs.ToListAsync();
    }
}
