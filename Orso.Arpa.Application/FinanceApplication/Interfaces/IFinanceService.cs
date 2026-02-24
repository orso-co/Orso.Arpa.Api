using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Application.FinanceApplication.Model;

namespace Orso.Arpa.Application.FinanceApplication.Interfaces
{
    public interface IFinanceService
    {
        Task<List<OrganizationAccountDto>> GetAccountsAsync(Guid? userId = null, bool isAdmin = true, CancellationToken cancellationToken = default);
        Task<OrganizationAccountDto> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<OrganizationAccountDto> CreateAccountAsync(CreateOrganizationAccountDto dto, CancellationToken cancellationToken = default);
        Task<OrganizationAccountDto> UpdateAccountAsync(Guid id, ModifyOrganizationAccountDto dto, CancellationToken cancellationToken = default);
        Task DeleteAccountAsync(Guid id, CancellationToken cancellationToken = default);

        Task<BalanceSummaryDto> GetBalanceSummaryAsync(Guid? userId = null, bool isAdmin = true, CancellationToken cancellationToken = default);
        Task<List<BalanceSnapshotDto>> GetBalanceHistoryAsync(Guid accountId, int days = 30, CancellationToken cancellationToken = default);
        Task TriggerSyncAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<List<PendingTanRequestDto>> GetPendingTanRequestsAsync(CancellationToken cancellationToken = default);
        Task SubmitTanAsync(SubmitTanDto dto, CancellationToken cancellationToken = default);

        // Finance Club Access
        Task<List<Guid>> GetAccessibleClubIdsForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> HasAnyFinanceAccessAsync(Guid userId, CancellationToken cancellationToken = default);
        Task GrantFinanceAccessAsync(Guid userId, Guid clubId, string grantedBy, CancellationToken cancellationToken = default);
        Task RevokeFinanceAccessAsync(Guid userId, Guid clubId, CancellationToken cancellationToken = default);
        Task<List<FinanceClubAccessDto>> GetAllFinanceAccessesAsync(CancellationToken cancellationToken = default);
    }
}
