using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.GenericHandlers
{
    public static class Details
    {
        public class Query<TEntity> : IRequest<TEntity> where TEntity : BaseEntity
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler<TEntity> : IRequestHandler<Query<TEntity>, TEntity> where TEntity : BaseEntity
        {
            private readonly IReadOnlyRepository _repository;

            public Handler(
                IReadOnlyRepository repository)
            {
                _repository = repository;
            }

            public async Task<TEntity> Handle(Query<TEntity> request, CancellationToken cancellationToken)
            {
                TEntity entity = await _repository.GetByIdAsync<TEntity>(request.Id);

                if (entity == null)
                {
                    throw new RestException($"{entity.GetType().Name} not found", HttpStatusCode.NotFound, new { Entity = "Not found" });
                }

                return entity;
            }
        }
    }
}
