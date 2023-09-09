using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.AuditLogApplication.Interfaces;
using Orso.Arpa.Application.AuditLogApplication.Model;
using Orso.Arpa.Domain.AuditLogDomain.Model;
using Orso.Arpa.Domain.General.GenericHandlers;

namespace Orso.Arpa.Application.AuditLogApplication.Services
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
                predicate: entityId == null ? null : v => v.KeyValues.Contains(entityId.ToString()),
                orderBy: v => v.OrderByDescending(v => v.CreatedAt),
                skip: skip ?? 0,
                take: take ?? 25));

            return _mapper.Map<IEnumerable<AuditLogDto>>(entities);
        }
    }
}
