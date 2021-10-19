using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Sections
{
    public static class Positions
    {
        public class Query : IRequest<IQueryable<SelectValueSection>>
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

        public class Handler : IRequestHandler<Query, IQueryable<SelectValueSection>>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public Task<IQueryable<SelectValueSection>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_arpaContext.Set<SelectValueSection>().AsQueryable().Where(s => s.SectionId.Equals(request.Id)));
            }
        }
    }
}
