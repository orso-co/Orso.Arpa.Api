using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.GraphQL;

namespace Orso.Arpa.Api.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        [UsePaging]
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<MusicianProfile>> GetMusicianProfiles([ScopedService] GraphQLContext context) =>
            new ValueTask<List<MusicianProfile>>(context.MusicianProfiles.ToListAsync());

        [UseApplicationDbContext]
        [UsePaging]
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Person>> GetPersons([ScopedService] GraphQLContext context) =>
            new ValueTask<List<Person>>(context.Persons.ToListAsync());

        [UseApplicationDbContext]
        [UsePaging]
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Project>> GetProjects([ScopedService] GraphQLContext context) =>
            new ValueTask<List<Project>>(context.Projects.ToListAsync());

        [UseApplicationDbContext]
        [UsePaging]
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<AuditLog>> GetAuditLogs([ScopedService] GraphQLContext context) =>
            new ValueTask<List<AuditLog>>(context.AuditLogs.ToListAsync());
    }
}
