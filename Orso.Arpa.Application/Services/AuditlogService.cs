using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AuditLogApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.GenericHandlers;

namespace Orso.Arpa.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediator;

        public AuditLogService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<AuditLogDto>> GetAsync(Guid? entityId, int? skip, int? take)
        {
            IQueryable<AuditLog> entities = await _mediator.Send(new List.Query<AuditLog>(
                predicate: v => v.KeyValues.Contains(entityId.ToString()),
                orderBy: v => v.OrderByDescending(v => v.CreatedAt),
                skip: skip ?? 0,
                take: take ?? 25));

            return _mapper.Map<IEnumerable<AuditLogDto>>(entities);
        }
    }
}
