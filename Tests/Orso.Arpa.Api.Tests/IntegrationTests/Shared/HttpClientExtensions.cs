using System.Net.Http;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal static class HttpClientExtensions
    {
        public static HttpClient AuthenticateWith(this HttpClient httpClient, User user)
        {
            httpClient.DefaultRequestHeaders.Add("username", user.UserName);
            return httpClient;
        }
    }
}
