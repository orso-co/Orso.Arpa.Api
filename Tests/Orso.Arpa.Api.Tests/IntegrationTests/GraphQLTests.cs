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
            IReadOnlyQueryRequest query = QueryRequestBuilder
                .New()
                .SetQuery("query Profiles {  musicianProfiles(skip: 0    take: 2    where: {isMainProfile: {equals: true}, and: {or: [{person: {givenName: {contains: \"\" }}}, {person: {surname: {contains: \"\" }}}, {instrument: {name: {contains: \"\" }}}]}}    order: {person: {givenName: ASC, surname: ASC}}  ) {    pageInfo {      hasNextPage      hasPreviousPage      __typename    }    totalCount    items {      id      isMainProfile      person {        id        givenName        surname        addresses {          country          zip          __typename        }        __typename      }      instrument {        id        name        __typename      }      __typename    }    __typename  }}")
                .Create();
            IExecutionResult result = await _executor.ExecuteAsync(query);
            _ = result.Errors.Should().BeNull();
            var queryResult = result as QueryResult;
            var serializedResult = JsonSerializer.Serialize(queryResult.Data);
            _ = serializedResult.Should().Be("{\"musicianProfiles\":{\"pageInfo\":{\"hasNextPage\":true,\"hasPreviousPage\":false,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":5,\"items\":[[{\"Name\":\"id\",\"Value\":\"7c215684-ee09-424f-9955-9b427494eaeb\",\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"isMainProfile\",\"Value\":true,\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"person\",\"Value\":{\"id\":\"56ed7c20-ba78-4a02-936e-5e840ef0748c\",\"givenName\":\"Initial\",\"surname\":\"Admin\",\"addresses\":[],\"__typename\":\"Person\"},\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"instrument\",\"Value\":{\"id\":\"7daa1394-a70d-4a24-88a6-ccf511d75c4d\",\"name\":\"Soprano\",\"__typename\":\"Section\"},\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"__typename\",\"Value\":\"MusicianProfile\",\"IsNullable\":false,\"IsInitialized\":true}],[{\"Name\":\"id\",\"Value\":\"9a609084-a5b6-485f-8960-724a8b470b13\",\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"isMainProfile\",\"Value\":true,\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"person\",\"Value\":{\"id\":\"cb441176-eecb-4c56-908d-5a6afec36a95\",\"givenName\":\"Per\",\"surname\":\"Former\",\"addresses\":[],\"__typename\":\"Person\"},\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"instrument\",\"Value\":{\"id\":\"a06431be-f9d6-44dc-8fdb-fbf8aa2bb940\",\"name\":\"Alto\",\"__typename\":\"Section\"},\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"__typename\",\"Value\":\"MusicianProfile\",\"IsNullable\":false,\"IsInitialized\":true}]],\"__typename\":\"MusicianProfileCollectionSegment\"}}");
        }

        [Test]
        public async Task Should_Query_AuditLogs()
        {
            IReadOnlyQueryRequest query = QueryRequestBuilder
                .New()
                .SetQuery("query AuditLog($skip: Int = 10, $take: Int = 2, $orderName: SortEnumType = DESC, $orderSurname: SortEnumType = DESC, $orderAboutMe: SortEnumType = DESC) { auditLogs( skip: $skip take: $take order: { createdAt: $orderName, tableName: $orderSurname, type: $orderAboutMe, createdBy: $orderAboutMe} ) { pageInfo { hasNextPage hasPreviousPage __typename } totalCount items { createdAt type tableName createdBy __typename } __typename }}")
                .Create();
            IExecutionResult result = await _executor.ExecuteAsync(query);
            _ = result.Errors.Should().BeNull();
            var queryResult = result as QueryResult;
            var serializedResult = JsonSerializer.Serialize(queryResult.Data);
            _ = serializedResult.Should().Be("{\"auditLogs\":{\"pageInfo\":{\"hasNextPage\":true,\"hasPreviousPage\":true,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":103,\"items\":[[{\"Name\":\"createdAt\",\"Value\":\"2030-02-02T00:00:00.000Z\",\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"type\",\"Value\":{\"Value\":\"CREATE\",\"HasValue\":true,\"IsEmpty\":false},\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"tableName\",\"Value\":\"User\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"createdBy\",\"Value\":\"anonymous\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"__typename\",\"Value\":\"AuditLog\",\"IsNullable\":false,\"IsInitialized\":true}],[{\"Name\":\"createdAt\",\"Value\":\"2030-02-02T00:00:00.000Z\",\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"type\",\"Value\":{\"Value\":\"CREATE\",\"HasValue\":true,\"IsEmpty\":false},\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"tableName\",\"Value\":\"User\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"createdBy\",\"Value\":\"anonymous\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"__typename\",\"Value\":\"AuditLog\",\"IsNullable\":false,\"IsInitialized\":true}]],\"__typename\":\"AuditLogCollectionSegment\"}}");
        }

        [Test]
        public async Task Should_Query_Persons()
        {
            IReadOnlyQueryRequest query = QueryRequestBuilder
            .New()
            .SetQuery("query Persons($skip: Int = 2, $take: Int = 1, $orderName: SortEnumType = ASC, $orderSurname: SortEnumType = ASC, $searchQuery: String = \"\") {  persons(    skip: $skip    take: $take    order: {surname: $orderSurname, givenName: $orderName}    where: {or: [{surname: {contains: $searchQuery}}]}  ) {    pageInfo {      hasNextPage      hasPreviousPage      __typename    }    totalCount    items {      id      givenName      surname      aboutMe      reliability      generalPreference      experienceLevel      createdAt      createdBy      modifiedAt      modifiedBy      __typename    }    __typename  }}")
            .Create();
            IExecutionResult result = await _executor.ExecuteAsync(query);
            _ = result.Errors.Should().BeNull();
            var queryResult = result as QueryResult;
            var serializedResult = JsonSerializer.Serialize(queryResult.Data);

            _ = serializedResult.Should().Be("{\"persons\":{\"pageInfo\":{\"hasNextPage\":true,\"hasPreviousPage\":true,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":12,\"items\":[[{\"Name\":\"id\",\"Value\":\"c0c8470b-e6a0-4a0b-8a4c-24d503636248\",\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"givenName\",\"Value\":\"Staff\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"surname\",\"Value\":\"Member\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"aboutMe\",\"Value\":null,\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"reliability\",\"Value\":0,\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"generalPreference\",\"Value\":0,\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"experienceLevel\",\"Value\":0,\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"createdAt\",\"Value\":\"2030-02-02T00:00:00.000Z\",\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"createdBy\",\"Value\":\"anonymous\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"modifiedAt\",\"Value\":null,\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"modifiedBy\",\"Value\":null,\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"__typename\",\"Value\":\"Person\",\"IsNullable\":false,\"IsInitialized\":true}]],\"__typename\":\"PersonCollectionSegment\"}}");
        }

        //[Test]
        //public async Task Should_Query_Projects()
        //{
        //    var data = new ResultMap();
        //    var innerData = new ResultMap();
        //    innerData.EnsureCapacity(4);
        //    var pageInfoMap = new ResultMap();
        //    pageInfoMap.EnsureCapacity(3);
        //    pageInfoMap.SetValue(0, "hasNextPage", true, false);
        //    pageInfoMap.SetValue(1, "hasPreviousPage", true, false);
        //    pageInfoMap.SetValue(2, "__typename", "CollectionSegmentInfo", false);
        //    var itemsMap = new ResultMap();
        //    itemsMap.EnsureCapacity(15);
        //    itemsMap.SetValue(0, "id", "b781c54d-8115-4561-b01e-9836fa05175e", false);
        //    itemsMap.SetValue(1, "title", "Die Schneekönigin", true);
        //    itemsMap.SetValue(2, "startDate", "2020-12-01T00:00:00.000+01:00");
        //    itemsMap.SetValue(3, "endDate", "2020-12-10T00:00:00.000+01:00");
        //    itemsMap.SetValue(4, "status", new NameString("PENDING"));
        //    itemsMap.SetValue(5, "isCompleted", false, false);
        //    itemsMap.SetValue(6, "genreId", "d733e38d-1d80-4054-b654-4ea4a128b0a8");
        //    var genreMap = new ResultMap();
        //    genreMap.EnsureCapacity(2);
        //    var selectValueMap = new ResultMap();
        //    selectValueMap.EnsureCapacity(2);
        //    selectValueMap.SetValue(0, "name", "Classical Music");
        //    selectValueMap.SetValue(1, "__typename", "SelectValue", false);
        //    genreMap.SetValue(0, "selectValue", selectValueMap);
        //    genreMap.SetValue(1, "__typename", "SelectValueMapping", false);
        //    itemsMap.SetValue(7, "genre", genreMap);
        //    itemsMap.SetValue(8, "typeId", "34f05f05-ef23-4f36-94e7-73b917530c51");
        //    itemsMap.SetValue(9, "parentId", null);
        //    itemsMap.SetValue(10, "shortTitle", "Schnee");
        //    itemsMap.SetValue(11, "description", "Let it snow");
        //    itemsMap.SetValue(12, "code", "1007");
        //    itemsMap.SetValue(13, "parent", null);
        //    itemsMap.SetValue(14, "__typename", "Project", false);
        //    innerData.SetValue(0, "pageInfo", pageInfoMap, false);
        //    innerData.SetValue(1, "totalCount", 5, false);
        //    innerData.SetValue(2, "items", new ResultMapList() { itemsMap }, true);
        //    innerData.SetValue(3, "__typename", "ProjectCollectionSegment", false);

        //    data.EnsureCapacity(1);
        //    data.SetValue(0, "projects", innerData);

        //    _ = new QueryResult(data, null);
        //    IReadOnlyQueryRequest query = QueryRequestBuilder
        //        .New()
        //        .SetQuery("query Projects($skip: Int = 2, $take: Int = 1, $orderTitle: SortEnumType = ASC, $orderStart: SortEnumType = ASC, $orderEnd: SortEnumType = ASC, $searchQuery: String = \"\") {  projects(    skip: $skip    take: $take    order: {title: $orderTitle, startDate: $orderStart, endDate: $orderEnd}    where: {or: [{title: {contains: $searchQuery}}, {code: {contains: $searchQuery}}, {shortTitle: {contains: $searchQuery}}]}  ) {    pageInfo {      hasNextPage      hasPreviousPage      __typename    }    totalCount    items {      id      title      startDate      endDate      status      isCompleted      genreId      genre {        selectValue {          name          __typename        }        __typename      }      typeId      parentId      shortTitle      description      code      parent {        title        id        __typename      }      __typename    }    __typename  }}")
        //        .Create();
        //    IExecutionResult result = await _executor.ExecuteAsync(query);
        //    _ = result.Errors.Should().BeNull();
        //    var queryResult = result as QueryResult;
        //    AssertionOptions.FormattingOptions.MaxDepth = 10;
        //    AssertionOptions.FormattingOptions.MaxLines = 200;
        //    _ = queryResult.Errors.Should().BeNull();
        //    _ = queryResult.Data.Count.Should().Be(1);
        //    _ = (queryResult.Data["projects"] as IEnumerable<ResultValue>).Should().BeEquivalentTo(innerData as IEnumerable<ResultValue>, opt => opt.ComparingByMembers<ResultValue>());
        //    //var serializedResult = JsonSerializer.Serialize(queryResult.Data);
        //    //_ = serializedResult.Should().Be("{\"projects\":{\"pageInfo\":{\"hasNextPage\":true,\"hasPreviousPage\":true,\"__typename\":\"CollectionSegmentInfo\"},\"totalCount\":5,\"items\":[[{\"Name\":\"id\",\"Value\":\"b781c54d-8115-4561-b01e-9836fa05175e\",\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"title\",\"Value\":\"Die Schneekönigin\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"startDate\",\"Value\":\"2020-12-01T00:00:00.000Z\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"endDate\",\"Value\":\"2020-12-10T00:00:00.000Z\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"status\",\"Value\":{\"Value\":\"PENDING\",\"HasValue\":true,\"IsEmpty\":false},\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"isCompleted\",\"Value\":false,\"IsNullable\":false,\"IsInitialized\":true},{\"Name\":\"genreId\",\"Value\":\"d733e38d-1d80-4054-b654-4ea4a128b0a8\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"genre\",\"Value\":{\"selectValue\":{\"name\":\"Classical Music\",\"__typename\":\"SelectValue\"},\"__typename\":\"SelectValueMapping\"},\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"typeId\",\"Value\":\"34f05f05-ef23-4f36-94e7-73b917530c51\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"parentId\",\"Value\":null,\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"shortTitle\",\"Value\":\"Schnee\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"description\",\"Value\":\"Let it snow\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"code\",\"Value\":\"1007\",\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"parent\",\"Value\":null,\"IsNullable\":true,\"IsInitialized\":true},{\"Name\":\"__typename\",\"Value\":\"Project\",\"IsNullable\":false,\"IsInitialized\":true}]],\"__typename\":\"ProjectCollectionSegment\"}}");
        //}
    }
}
