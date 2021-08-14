using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.GraphQL;

namespace Orso.Arpa.Api.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        public ValueTask<List<MusicianProfile>> GetMusicianProfiles([ScopedService] GraphQLContext context) =>
            context.MusicianProfiles.ToListAsync();
    }
}
