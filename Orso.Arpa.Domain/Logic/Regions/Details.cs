using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Regions
{
    public static class Details
    {
        public class Query : IRequest<Region>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Region>
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public async Task<Region> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetByIdAsync<Region>(request.Id);
            }
        }
    }
}
