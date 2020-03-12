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
    public static class Delete
    {
        public class Command<TEntity> : IRequest where TEntity : BaseEntity
        {
            public Guid Id { get; set; }
        }

        public class Handler<TEntity> : IRequestHandler<Command<TEntity>> where TEntity : BaseEntity
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command<TEntity> request, CancellationToken cancellationToken)
            {
                TEntity entityToDelete = await _arpaContext.FindAsync<TEntity>(new object[] { request.Id }, cancellationToken);

                if (entityToDelete == null)
                {
                    var entityName = typeof(TEntity).Name;
                    throw new RestException($"{entityName} not found", HttpStatusCode.NotFound, new
                    {
                        Entity = "Not found"
                    });
                }

                _arpaContext.Remove(entityToDelete);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem deleting {typeof(TEntity).Name}");
            }
        }
    }
}
