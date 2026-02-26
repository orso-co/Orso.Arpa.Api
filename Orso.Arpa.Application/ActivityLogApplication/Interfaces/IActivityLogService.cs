using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Application.ActivityLogApplication.Model;

namespace Orso.Arpa.Application.ActivityLogApplication.Interfaces;

public interface IActivityLogService
{
    Task<ActivityLogResultDto> GetLogsAsync(ActivityLogFilterDto filter, CancellationToken ct = default);
    Task<ActivityLogStatsDto> GetStatsAsync(CancellationToken ct = default);
    Task<List<ActivityLogUserDto>> GetDistinctUsersAsync(CancellationToken ct = default);
    Task CreateBatchAsync(List<CreateActivityLogDto> events, Guid userId, string username, CancellationToken ct = default);
    Task CleanupOldLogsAsync(int retentionDays = 90, CancellationToken ct = default);
}
