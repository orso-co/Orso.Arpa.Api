using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.GenericHandlers
{
    public static class Modify
    {
        public interface IModifyCommand<TEntity> : IRequest where TEntity : BaseEntity
        {
            public Guid Id { get; set; }
        }

        public class Handler<TEntity> : IRequestHandler<IModifyCommand<TEntity>> where TEntity : BaseEntity
        {
            private readonly IRepository _repository;
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(IModifyCommand<TEntity> request, CancellationToken cancellationToken)
            {
                TEntity existingEntity = await _repository.GetByIdAsync<TEntity>(request.Id);

                TEntity modifiedEntity = _mapper.Map(request, existingEntity);

                _repository.Update(modifiedEntity);

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem updating {existingEntity.GetType().Name}");
            }
        }
    }
}
