using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.Extensions.EntityCreator;

namespace Orso.Arpa.Domain.GenericHandlers
{
    public static class Create
    {
        public interface ICreateCommand<TEntity> : IRequest<TEntity> where TEntity : BaseEntity
        {
        }

        public class Handler<TEntity> : IRequestHandler<ICreateCommand<TEntity>, TEntity> where TEntity : BaseEntity
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<TEntity> Handle(ICreateCommand<TEntity> request, CancellationToken cancellationToken)
            {
                ConstructorInfo ctor = typeof(TEntity).GetConstructors()
                    .First(c => c.IsPublic);

                ObjectActivator<TEntity> createdActivator = GetActivator<TEntity>(ctor);

                TEntity newEntity = createdActivator(Guid.NewGuid(), request);

                EntityEntry<TEntity> createResult = _arpaContext.Add(newEntity);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return createResult.Entity;
                }

                throw new Exception($"Problem creating {newEntity.GetType().Name}");
            }
        }
    }
}
