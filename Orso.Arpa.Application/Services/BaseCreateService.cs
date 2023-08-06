using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Application.Services
{
    public abstract class BaseCreateService<TGetDto, TEntity, TCreateDto, TCreateCommand> : BaseReadOnlyService<TGetDto, TEntity>
        where TEntity : BaseEntity
        where TCreateCommand : ICreateCommand<TEntity>
    {
        protected BaseCreateService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public virtual async Task<TGetDto> CreateAsync(TCreateDto createDto)
        {
            TCreateCommand command = _mapper.Map<TCreateCommand>(createDto);
            TEntity createdEntity = await _mediator.Send(command);
            return _mapper.Map<TGetDto>(createdEntity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            Type entityDeleteNotificationType = Array.Find(Assembly
                .GetAssembly(typeof(BaseEntity))
                .GetTypes(), t => Array.Exists(t.GetInterfaces(), x =>
                                    x.IsGenericType &&
                                    x.GetGenericTypeDefinition() == typeof(IEntityDeleteNotification<>) &&
                                    x.GetGenericArguments().Contains(typeof(TEntity))));

            if (entityDeleteNotificationType != null)
            {
                var notification = Activator.CreateInstance(entityDeleteNotificationType) as IEntityDeleteNotification<TEntity>;
                notification.Id = id;
                await _mediator.Publish(notification);
            }

            _ = await _mediator.Send(new Delete.Command<TEntity>() { Id = id });
        }
    }
}
