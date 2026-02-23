using System.Threading;
using System.Threading.Tasks;

namespace Orso.Arpa.Domain.FinanceDomain.Interfaces
{
    public interface IPayPalBalanceService
    {
        Task<BalanceResult> GetBalanceAsync(string encryptedCredentials, CancellationToken cancellationToken);
    }

    public record BalanceResult(
        decimal Balance,
        decimal? AvailableBalance,
        string Currency,
        bool Success,
        string ErrorMessage = null);
}
