using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.FinanceDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.Finance
{
    public class PayPalBalanceService : IPayPalBalanceService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICredentialEncryptionService _encryptionService;
        private readonly ILogger<PayPalBalanceService> _logger;

        public PayPalBalanceService(
            IHttpClientFactory httpClientFactory,
            ICredentialEncryptionService encryptionService,
            ILogger<PayPalBalanceService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _encryptionService = encryptionService;
            _logger = logger;
        }

        public async Task<BalanceResult> GetBalanceAsync(string encryptedCredentials, CancellationToken cancellationToken)
        {
            try
            {
                var json = _encryptionService.Decrypt(encryptedCredentials);
                if (json == null)
                    return new BalanceResult(0, null, "EUR", false, "Failed to decrypt credentials");

                var credentials = JsonSerializer.Deserialize<PayPalCredentials>(json);
                if (credentials == null)
                    return new BalanceResult(0, null, "EUR", false, "Invalid credentials format");

                var baseUrl = credentials.UseSandbox
                    ? "https://api-m.sandbox.paypal.com"
                    : "https://api-m.paypal.com";

                var client = _httpClientFactory.CreateClient();

                var accessToken = await GetAccessTokenAsync(client, baseUrl, credentials.ClientId, credentials.ClientSecret, cancellationToken);
                if (accessToken == null)
                    return new BalanceResult(0, null, "EUR", false, "Failed to obtain PayPal access token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync($"{baseUrl}/v1/reporting/balances?currency_code=EUR", cancellationToken);
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("PayPal balance API returned {StatusCode}: {Content}", response.StatusCode, responseContent);
                    return new BalanceResult(0, null, "EUR", false, $"PayPal API error: {response.StatusCode}");
                }

                using var doc = JsonDocument.Parse(responseContent);
                var balances = doc.RootElement.GetProperty("balances");

                decimal totalBalance = 0;
                decimal? availableBalance = null;
                string currency = "EUR";

                foreach (var balance in balances.EnumerateArray())
                {
                    currency = balance.GetProperty("currency").GetString();
                    if (balance.TryGetProperty("total_balance", out var total))
                    {
                        totalBalance = decimal.Parse(total.GetProperty("value").GetString());
                    }
                    if (balance.TryGetProperty("available_balance", out var available))
                    {
                        availableBalance = decimal.Parse(available.GetProperty("value").GetString());
                    }
                    break;
                }

                return new BalanceResult(totalBalance, availableBalance, currency, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching PayPal balance");
                return new BalanceResult(0, null, "EUR", false, ex.Message);
            }
        }

        private static async Task<string> GetAccessTokenAsync(
            HttpClient client, string baseUrl, string clientId, string clientSecret,
            CancellationToken cancellationToken)
        {
            var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

            var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync($"{baseUrl}/v1/oauth2/token", content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(responseContent);
            return doc.RootElement.GetProperty("access_token").GetString();
        }
    }

    internal class PayPalCredentials
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool UseSandbox { get; set; }
    }
}
