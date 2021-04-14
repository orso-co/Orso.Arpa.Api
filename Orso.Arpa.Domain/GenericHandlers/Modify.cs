using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
            private readonly IArpaContext _context;
            private readonly IMapper _mapper;

            public Handler(
                IArpaContext context,
                IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(IModifyCommand<TEntity> request, CancellationToken cancellationToken)
            {
                TEntity existingEntity = await _context.FindAsync<TEntity>(new object[] { request.Id }, cancellationToken);

                if (existingEntity == null)
                {
                    throw new ValidationException(new[] { new ValidationFailure(nameof(request.Id), $"The {typeof(TEntity).Name} could not be found.") });
                }

                TEntity modifiedEntity = _mapper.Map(request, existingEntity);

                _context.Entry(existingEntity)?.CurrentValues?.SetValues(modifiedEntity);

                if (await _context.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem updating {existingEntity.GetType().Name}");
            }
        }
    }
}
