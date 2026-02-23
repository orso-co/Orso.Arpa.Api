using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orso.Arpa.Application.FinanceApplication.Model;

namespace Orso.Arpa.Application.FinanceApplication.Interfaces
{
    public interface IFinanceService
    {
        Task<List<OrganizationAccountDto>> GetAccountsAsync(CancellationToken cancellationToken = default);
        Task<OrganizationAccountDto> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<OrganizationAccountDto> CreateAccountAsync(CreateOrganizationAccountDto dto, CancellationToken cancellationToken = default);
        Task<OrganizationAccountDto> UpdateAccountAsync(Guid id, ModifyOrganizationAccountDto dto, CancellationToken cancellationToken = default);
        Task DeleteAccountAsync(Guid id, CancellationToken cancellationToken = default);

        Task<BalanceSummaryDto> GetBalanceSummaryAsync(CancellationToken cancellationToken = default);
        Task<List<BalanceSnapshotDto>> GetBalanceHistoryAsync(Guid accountId, int days = 30, CancellationToken cancellationToken = default);
        Task TriggerSyncAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<List<PendingTanRequestDto>> GetPendingTanRequestsAsync(CancellationToken cancellationToken = default);
        Task SubmitTanAsync(SubmitTanDto dto, CancellationToken cancellationToken = default);
    }
}
