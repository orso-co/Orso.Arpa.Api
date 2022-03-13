using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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
            Guid Id { get; }
        }

        public class Handler<TEntity> : IRequestHandler<IModifyCommand<TEntity>> where TEntity : BaseEntity
        {
            private readonly IArpaContext _context;

            public Handler(
                IArpaContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(IModifyCommand<TEntity> request, CancellationToken cancellationToken)
            {
                TEntity existingEntity = await _context.FindAsync<TEntity>(new object[] { request.Id }, cancellationToken);

                if (existingEntity == null)
                {
                    throw new ValidationException(new[]
                    {
                        new ValidationFailure(nameof(request.Id), $"The {typeof(TEntity).Name} could not be found.")
                        {
                            ErrorCode = "404"
                        }
                    });
                }

                MethodInfo updateMethod = typeof(TEntity).GetMethod("Update", BindingFlags.Public | BindingFlags.Instance, new Type[] { request.GetType() });
                if (updateMethod is null)
                {
                    throw new Exception($"No 'Update' method found for type '{typeof(TEntity)}' and command '{request.GetType()}");
                }

                updateMethod.Invoke(existingEntity, new[] { request });

                _context.Entry(existingEntity)?.CurrentValues?.SetValues(existingEntity);

                if (await _context.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception($"Problem updating {existingEntity.GetType().Name}");
            }
        }
    }
}
