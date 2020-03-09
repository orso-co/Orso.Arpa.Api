using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Entities;
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
            private readonly IRepository _repository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command<TEntity> request, CancellationToken cancellationToken)
            {
                await _repository.DeleteAsync<TEntity>(request.Id);

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem deleting {typeof(TEntity).Name}");
            }
        }
    }
}
