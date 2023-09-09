using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.AuditLogApplication.Model;

namespace Orso.Arpa.Application.AuditLogApplication.Interfaces
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAsync(Guid? entityId, int? skip, int? take);
    }
}
