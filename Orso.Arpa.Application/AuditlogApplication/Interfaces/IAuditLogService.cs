using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.AuditLogApplication.Model;

namespace Orso.Arpa.Application.AuditLogApplication.Interfaces
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAsync(Guid? entityId, int? skip, int? take);

        /// <summary>
        /// Gets recent participation changes (project and appointment) with resolved names.
        /// Used for the Dashboard activity feed.
        /// </summary>
        /// <param name="take">Number of items to return (default 20)</param>
        /// <returns>List of recent participation changes with resolved person, project, and appointment names</returns>
        Task<IEnumerable<RecentParticipationChangeDto>> GetRecentParticipationChangesAsync(int take = 20);
    }
}
