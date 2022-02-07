using System.Threading.Tasks;
using HotChocolate.Execution;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class GraphQLTests : IntegrationTestBase
    {
        [Test]
        public async Task Foo()
        {
            IRequestExecutor executor = await Startup.RequestExecutorBuilder.BuildRequestExecutorAsync();
            IReadOnlyQueryRequest query = QueryRequestBuilder
                .New()
                .SetQuery("{\"operationName\":\"Profiles\",\"variables\":{\"order\":\"ASC\",\"searchQuery\":\"\",\"take\":50},\"query\":\"query Profiles($skip: Int, $take: Int, $order: SortEnumType = ASC, $searchQuery: String = \"\") {\n  musicianProfiles(\n    skip: $skip\n    take: $take\n    where: {isMainProfile: {equals: true}, and: {or: [{person: {givenName: {contains: $searchQuery}}}, {person: {surname: {contains: $searchQuery}}}, {instrument: {name: {contains: $searchQuery}}}]}}\n    order: {person: {givenName: $order, surname: $order}}\n  ) {\n    pageInfo {\n      hasNextPage\n      hasPreviousPage\n      __typename\n    }\n    totalCount\n    items {\n      id\n      isMainProfile\n      person {\n        id\n        givenName\n        surname\n        addresses {\n          country\n          zip\n          __typename\n        }\n        __typename\n      }\n      instrument {\n        id\n        name\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n\"}")
                // you can also add a test principal if you want to test authorised
                // resolvers
                // .AddProperty(nameof(ClaimsPrincipal), CreatePrincipal())
                .Create();
            IExecutionResult result = await executor.ExecuteAsync(query);
        }
    }
}
