using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;

namespace Orso.Arpa.Application.Services
{
    public abstract class BaseReadOnlyService<TGetDto, TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;

        protected BaseReadOnlyService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
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

            // Don't use ProjectTo -> AfterMap actions won't be executed
            List<TEntity> list = await entities.ToListAsync();
            return _mapper.Map<IEnumerable<TGetDto>>(list);
        }

        public virtual async Task<TGetDto> GetByIdAsync(Guid id)
        {
            TEntity entity = await _mediator.Send(new Details.Query<TEntity>(id));
            return _mapper.Map<TGetDto>(entity);
        }
    }
}
