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
        public ValueTask<List<MusicianProfile>> GetMusicianProfiles(GraphQLContext context) =>
            new(context.MusicianProfiles.ToListAsync());

        [UseApplicationDbContext]
        [UsePaging]
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Person>> GetPersons(GraphQLContext context) =>
            new(context.Persons.ToListAsync());

        [UseApplicationDbContext]
        [UsePaging]
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<Project>> GetProjects(GraphQLContext context) =>
            new(context.Projects.ToListAsync());

        [UseApplicationDbContext]
        [UsePaging]
        [UseOffsetPaging]
        [UseFiltering]
        [UseSorting]
        public ValueTask<List<AuditLog>> GetAuditLogs(GraphQLContext context) =>
            new(context.AuditLogs.ToListAsync());
    }
}
