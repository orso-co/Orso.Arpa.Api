using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.SectionDomain.Queries
{
    public static class ListDoublingInstruments
    {
        public class Query : IRequest<IEnumerable<Section>>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(q => q.Id)
                    .EntityExists<Query, Section>(arpaContext);
            }
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
                Section section = await _arpaContext.FindAsync<Section>(new object[] { request.Id }, cancellationToken);
                if (section.IsInstrument)
                {
                    return section.Children;
                }
                Section parent = GetInstrumentParent(section);

                if (parent is null)
                {
                    return new List<Section>();
                }

                var list = new List<Section>
                {
                    parent
                };
                list = list.Union(parent.Children).ToList();
                list.Remove(section);

                return list;
            }

            private static Section GetInstrumentParent(Section section)
            {
                Section parent = section.Parent;
                if (parent is null)
                {
                    return null;
                }
                return parent.IsInstrument ? parent : GetInstrumentParent(parent);
            }
        }
    }
}
