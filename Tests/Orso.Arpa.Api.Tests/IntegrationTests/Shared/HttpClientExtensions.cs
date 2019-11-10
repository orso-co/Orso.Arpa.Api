using System.Net.Http;
using Newtonsoft.Json;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal static class HttpClientExtensions
    {
        public static HttpClient AuthenticateWith(this HttpClient httpClient, User user)
        {
            httpClient.DefaultRequestHeaders.Add("user", JsonConvert.SerializeObject(user));
            return httpClient;
        }
    }
}
