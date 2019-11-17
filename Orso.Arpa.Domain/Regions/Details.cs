using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Regions
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
                Region region = await _repository.GetByIdAsync<Region>(request.Id);
                if (region == null)
                {
                    throw new RestException("Region not found", HttpStatusCode.NotFound, new { Region = "Not found" });
                }
                return region;
            }
        }
    }
}
