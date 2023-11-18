using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HotChocolate.Execution;
using NUnit.Framework;
using Orso.Arpa.Api.Tests.IntegrationTests.Shared;

namespace Orso.Arpa.Api.Tests.IntegrationTests
{
    [TestFixture]
    public class GraphQLTests : IntegrationTestBase
    {
        private IRequestExecutor _executor;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _executor = await Startup.RequestExecutorBuilder.BuildRequestExecutorAsync();
        }

        [Test]
        public async Task Should_Query_MusicianProfiles()
        {
            IQueryRequest query = QueryRequestBuilder
                .New()
                .SetQuery("query Profiles {  musicianProfiles(skip: 0    take: 2    where: {isMainProfile: {equals: true}, and: {or: [{person: {givenName: {contains: \"\" }}}, {person: {surname: {contains: \"\" }}}, {instrument: {name: {contains: \"\" }}}]}}    order: {person: {givenName: ASC, surname: ASC}}  ) {    pageInfo {      hasNextPage      hasPreviousPage      __typename    }    totalCount    items {      id      isMainProfile      person {        id        givenName        surname        addresses {          country          zip          __typename        }        __typename      }      instrument {        id        name        __typename      }      __typename    }    __typename  }}")
                .Create();
            IExecutionResult result = await _executor.ExecuteAsync(query);
            var queryResult = result as QueryResult;
            _ = queryResult.Errors.Should().BeNull();
            var serializedResult = JsonSerializer.Serialize(queryResult.Data);
            _ = serializedResult.Should().Be("{\"musicianProfiles\":" +
                "{\"pageInfo\":{\"hasNextPage\":true,\"hasPreviousPage\":false,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":5,\"items\":[" +
                "{\"id\":\"7c215684-ee09-424f-9955-9b427494eaeb\",\"isMainProfile\":true,\"person\":{\"id\":\"56ed7c20-ba78-4a02-936e-5e840ef0748c\",\"givenName\":\"Initial\",\"surname\":\"Admin\",\"addresses\":[],\"__typename\":\"Person\"},\"instrument\":{\"id\":\"7daa1394-a70d-4a24-88a6-ccf511d75c4d\",\"name\":\"Soprano\",\"__typename\":\"Section\"},\"__typename\":\"MusicianProfile\"}," +
                "{\"id\":\"9a609084-a5b6-485f-8960-724a8b470b13\",\"isMainProfile\":true,\"person\":{\"id\":\"cb441176-eecb-4c56-908d-5a6afec36a95\",\"givenName\":\"Per\",\"surname\":\"Former\",\"addresses\":[],\"__typename\":\"Person\"},\"instrument\":{\"id\":\"a06431be-f9d6-44dc-8fdb-fbf8aa2bb940\",\"name\":\"Alto\",\"__typename\":\"Section\"},\"__typename\":\"MusicianProfile\"}]," +
                "\"__typename\":\"MusicianProfilesCollectionSegment\"}}");
        }

        [Test]
        public async Task Should_Query_AuditLogs()
        {
            IQueryRequest query = QueryRequestBuilder
                .New()
                .SetQuery("query AuditLog($skip: Int = 10, $take: Int = 2, $orderName: SortEnumType = DESC, $orderSurname: SortEnumType = DESC, $orderAboutMe: SortEnumType = DESC) { auditLogs( skip: $skip take: $take order: { createdAt: $orderName, tableName: $orderSurname, type: $orderAboutMe, createdBy: $orderAboutMe} ) { pageInfo { hasNextPage hasPreviousPage __typename } totalCount items { type tableName createdBy __typename } __typename }}")
                .Create();
            IExecutionResult result = await _executor.ExecuteAsync(query);
            var queryResult = result as QueryResult;
            _ = queryResult.Errors.Should().BeNull();
            var serializedResult = JsonSerializer.Serialize(queryResult.Data);
            _ = serializedResult.Should().Be("{\"auditLogs\":" +
                "{\"pageInfo\":{\"hasNextPage\":true,\"hasPreviousPage\":true,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":111,\"items\":[" +
                "{\"type\":\"UPDATE\",\"tableName\":\"User\",\"createdBy\":\"anonymous\",\"__typename\":\"AuditLog\"}," +
                "{\"type\":\"CREATE\",\"tableName\":\"User\",\"createdBy\":\"anonymous\",\"__typename\":\"AuditLog\"}]," +
                "\"__typename\":\"AuditLogsCollectionSegment\"}}");
        }

        [Test]
        public async Task Should_Query_Persons()
        {
            IQueryRequest query = QueryRequestBuilder
            .New()
            .SetQuery("query Persons($skip: Int = 2, $take: Int = 1, $orderName: SortEnumType = ASC, $orderSurname: SortEnumType = ASC, $searchQuery: String = \"\") {  persons(    skip: $skip    take: $take    order: {surname: $orderSurname, givenName: $orderName}    where: {or: [{surname: {contains: $searchQuery}}]}  ) {    pageInfo {      hasNextPage      hasPreviousPage      __typename    }    totalCount    items {      id      givenName      surname      aboutMe      reliability      generalPreference      experienceLevel      createdBy      modifiedBy      __typename    }    __typename  }}")
            .Create();
            IExecutionResult result = await _executor.ExecuteAsync(query);
            var queryResult = result as QueryResult;
            _ = queryResult.Errors.Should().BeNull();
            var serializedResult = JsonSerializer.Serialize(queryResult.Data);

            _ = serializedResult.Should().Be("{\"persons\":" +
                "{\"pageInfo\":{\"hasNextPage\":true,\"hasPreviousPage\":true,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":12,\"items\":[" +
                "{\"id\":\"c0c8470b-e6a0-4a0b-8a4c-24d503636248\",\"givenName\":\"Staff\",\"surname\":\"Member\",\"aboutMe\":null,\"reliability\":0,\"generalPreference\":0,\"experienceLevel\":0,\"createdBy\":\"anonymous\",\"modifiedBy\":null,\"__typename\":\"Person\"}]," +
                "\"__typename\":\"PersonsCollectionSegment\"}}");
        }

        [Test]
        public async Task Should_Query_Projects()
        {
            IQueryRequest query = QueryRequestBuilder
                .New()
                .SetQuery("query Projects($skip: Int = 6, $take: Int = 1, $orderTitle: SortEnumType = ASC, $orderStart: SortEnumType = ASC, $orderEnd: SortEnumType = ASC, $searchQuery: String = \"\") {  projects(    skip: $skip    take: $take    order: {title: $orderTitle, startDate: $orderStart, endDate: $orderEnd}    where: {or: [{title: {contains: $searchQuery}}, {code: {contains: $searchQuery}}, {shortTitle: {contains: $searchQuery}}]}  ) {    pageInfo {      hasNextPage      hasPreviousPage      __typename    }    totalCount    items {      id      title      status      isCompleted      genreId      genre {        selectValue {          name          __typename        }        __typename      }      typeId      parentId      shortTitle      description      code      parent {        title        id        __typename      }      __typename    }    __typename  }}")
                .Create();
            IExecutionResult result = await _executor.ExecuteAsync(query);
            var queryResult = result as QueryResult;
            _ = queryResult.Errors.Should().BeNull();
            var serializedResult = JsonSerializer.Serialize(queryResult.Data);
            _ = serializedResult.Should().Be("{\"projects\":" +
                "{\"pageInfo\":{\"hasNextPage\":false,\"hasPreviousPage\":true,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":7,\"items\":[" +
                "{\"id\":\"a19d84f1-4ac1-49c3-abfe-527092b80b6d\",\"title\":\"Rocking X-mas Freiburg\",\"status\":\"PENDING\",\"isCompleted\":true,\"genreId\":\"d733e38d-1d80-4054-b654-4ea4a128b0a8\",\"genre\":{\"selectValue\":{\"name\":\"Classical Music\",\"__typename\":\"SelectValue\"},\"__typename\":\"SelectValueMapping\"},\"typeId\":\"34f05f05-ef23-4f36-94e7-73b917530c51\",\"parentId\":null,\"shortTitle\":\"RockXmas\",\"description\":\"Rocking around the christmas tree\",\"code\":\"1005\",\"parent\":null,\"__typename\":\"Project\"}]" +
                ",\"__typename\":\"ProjectsCollectionSegment\"}}");
        }
    }
}
