using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Registers
{
    public static class List
    {
        public class Query : IRequest<IImmutableList<Register>>
        {
        }

        public class Handler : IRequestHandler<Query, IImmutableList<Register>>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public Task<IImmutableList<Register>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_repository.GetAll<Register>().ToImmutableList() as IImmutableList<Register>);
            }
        }
    }
}
