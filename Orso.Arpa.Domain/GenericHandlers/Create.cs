using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            private readonly IRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<TEntity> Handle(ICreateCommand<TEntity> request, CancellationToken cancellationToken)
            {
                ConstructorInfo ctor = typeof(TEntity).GetConstructors()
                    .First(c => c.IsPublic);

                ObjectActivator<TEntity> createdActivator = GetActivator<TEntity>(ctor);

                TEntity newEntity = createdActivator(Guid.NewGuid(), request);

                TEntity createdEntity = await _repository.AddAsync(newEntity);

                if (await _unitOfWork.CommitAsync())
                {
                    return createdEntity;
                }

                throw new Exception($"Problem creating {newEntity.GetType().Name}");
            }
        }
    }
}
