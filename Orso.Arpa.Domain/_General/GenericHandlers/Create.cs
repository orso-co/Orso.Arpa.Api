using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.General.Model;
using static Orso.Arpa.Domain.General.Extensions.EntityCreator;

namespace Orso.Arpa.Domain.General.GenericHandlers
{
    public static class Create
    {
        public interface ICreateCommand<out TEntity> : IRequest<TEntity> where TEntity : BaseEntity
        {
        }

        public class Handler<TEntity> : IRequestHandler<ICreateCommand<TEntity>, TEntity> where TEntity : BaseEntity
        {
            protected readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public virtual async Task<TEntity> Handle(ICreateCommand<TEntity> request, CancellationToken cancellationToken)
            {
                ConstructorInfo ctor = typeof(TEntity).GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new Type[] { typeof(Guid?), request.GetType() },
                    null);

                ObjectActivator<TEntity> createdActivator = GetActivator<TEntity>(ctor);

                TEntity newEntity = createdActivator(Guid.NewGuid(), request);

                EntityEntry<TEntity> createResult = _arpaContext.Add(newEntity);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    // if we return createResult.Entity directly, some navigation properties may not be loaded properly
                    _arpaContext.ClearChangeTracker();
                    return await _arpaContext.Set<TEntity>().FindAsync(new object[] { createResult.Entity.Id }, cancellationToken);
                }

                throw new AffectedRowCountMismatchException(newEntity.GetType().Name);
            }
        }
    }
}
