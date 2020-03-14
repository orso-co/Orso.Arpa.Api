using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.GenericHandlers
{
    public static class List
    {
        public class Query<TEntity> : IRequest<IQueryable<TEntity>> where TEntity : BaseEntity
        {
            public Query(
                Expression<Func<TEntity, bool>> predicate = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                int? skip = null,
                int? take = null)
            {
                OrderBy = orderBy;
                Skip = skip;
                Take = take;
                Predicate = predicate;
            }

            public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
            public int? Skip { get; }
            public int? Take { get; }
            public Expression<Func<TEntity, bool>> Predicate { get; }
        }

        public class Handler<TEntity> : IRequestHandler<Query<TEntity>, IQueryable<TEntity>> where TEntity : BaseEntity
        {
            private readonly IArpaContext _context;

            public Handler(IArpaContext context)
            {
                _context = context;
            }

            public Task<IQueryable<TEntity>> Handle(Query<TEntity> request, CancellationToken cancellationToken)
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();

                if (request.Predicate != null)
                {
                    query = query.Where(request.Predicate);
                }

                if (request.OrderBy != null)
                {
                    query = request.OrderBy(query);
                }

                if (request.Skip.HasValue)
                {
                    query = query.Skip(request.Skip.Value);
                }

                if (request.Take.HasValue)
                {
                    query = query.Take(request.Take.Value);
                }

                return Task.FromResult(query);
            }
        }
    }
}
