using System;
using System.Threading;
using System.Threading.Tasks;

namespace Orso.Arpa.Domain.FinanceDomain.Interfaces
{
    public interface IFinTsBalanceService
    {
        Task<FinTsBalanceResult> GetBalanceAsync(
            string encryptedCredentials,
            CancellationToken cancellationToken);

        Task<FinTsBalanceResult> SubmitTanAndGetBalanceAsync(
            Guid tanRequestId,
            string tan,
            CancellationToken cancellationToken);
    }

    public record FinTsBalanceResult(
        decimal? Balance,
        decimal? AvailableBalance,
        string Currency,
        bool Success,
        bool TanRequired,
        string TanChallenge = null,
        string TanMediumName = null,
        Guid? TanRequestId = null,
        string ErrorMessage = null);
}
