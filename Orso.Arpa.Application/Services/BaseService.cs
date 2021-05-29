using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;
using static Orso.Arpa.Domain.GenericHandlers.Create;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Application.Services
{
    public abstract class BaseService<TGetDto, TEntity, TCreateDto, TCreateCommand, TModifyDto, TModifyBodyDto, TModifyCommand> : BaseReadOnlyService<TGetDto, TEntity>
        where TEntity : BaseEntity
        where TCreateCommand : ICreateCommand<TEntity>
        where TModifyDto : IdFromRouteDto<TModifyBodyDto>
        where TModifyCommand : IModifyCommand<TEntity>
    {

        protected BaseService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public virtual async Task<TGetDto> CreateAsync(TCreateDto createDto)
        {
            TCreateCommand command = _mapper.Map<TCreateCommand>(createDto);
            TEntity createdEntity = await _mediator.Send(command);
            return _mapper.Map<TGetDto>(createdEntity);
        }

        public virtual async Task ModifyAsync(TModifyDto modifyDto)
        {
            TModifyCommand command = _mapper.Map<TModifyCommand>(modifyDto);
            await _mediator.Send(command);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new Delete.Command<TEntity>() { Id = id });
        }
    }
}
