using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Sections
{
    public static class Tree
    {
        public class Query : IRequest<ITree<Section>>
        {
            public int? MaxLevel { get; set; }
        }

        public class Handler : IRequestHandler<Query, ITree<Section>>
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<ITree<Section>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Section> sections = await _context.Sections.ToListAsync(cancellationToken);
                return sections.ToTree((parent, child) => child.ParentId == parent.Id, request.MaxLevel);
            }
        }
    }
}
