using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;
using static Orso.Arpa.Domain.GenericHandlers.Create;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Application.Services
{
    public abstract class BaseService<TGetDto, TEntity, TCreateDto, TCreateCommand, TModifyDto, TModifyCommand>
        where TEntity : BaseEntity
        where TCreateCommand : ICreateCommand<TEntity>
        where TModifyCommand : IModifyCommand<TEntity>
        where TModifyDto : IModifyDto
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;

        protected BaseService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public virtual async Task<TGetDto> CreateAsync(TCreateDto createDto)
        {
            TCreateCommand command = _mapper.Map<TCreateCommand>(createDto);
            TEntity createdEntity = await _mediator.Send(command);
            return _mapper.Map<TGetDto>(createdEntity);
        }

        public virtual async Task<IEnumerable<TGetDto>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<TEntity> entities = await _mediator.Send(new List.Query<TEntity>(
                predicate: predicate,
                orderBy: orderBy,
                skip: skip,
                take: take));

            return entities
                .ProjectTo<TGetDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
        }

        public virtual async Task<TGetDto> GetByIdAsync(Guid id)
        {
            TEntity entity = await _mediator.Send(new Details.Query<TEntity>(id));
            TGetDto dto = _mapper.Map<TGetDto>(entity);
            return dto;
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
