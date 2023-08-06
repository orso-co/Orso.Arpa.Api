using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Sections
{
    public static class InstrumentsWithChildren
    {
        public class Query : IRequest<IEnumerable<Section>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Section>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }
            public async Task<IEnumerable<Section>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Section> sections = await _arpaContext.Sections
                    .Where(s => s.IsInstrument)
                    .ToListAsync(cancellationToken);

                var result = new List<Section>();
                result.AddRange(sections);
                foreach (Section section in sections)
                {
                    AddChildren(section, result);
                }
                return result.OrderBy(s => s.Name).ToList();
            }

            private static void AddChildren(Section section, List<Section> result)
            {
                result.AddRange(section.Children);
                foreach (Section child in section.Children)
                {
                    AddChildren(child, result);
                }
            }
        }
    }
}
