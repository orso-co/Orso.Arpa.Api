using System;
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
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public async Task<TEntity> Handle(Query<TEntity> request, CancellationToken cancellationToken)
            {
                TEntity entity = await _context
                    .FindAsync<TEntity>(new object[] { request.Id }, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(typeof(TEntity).Name, nameof(request.Id));
                }

                return entity;
            }
        }
    }
}
