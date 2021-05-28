using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.AuditLogApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAsync(Guid? entityId, int? skip, int? take);
    }
}
