using System;
using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Logic.Urls;
using Orso.Arpa.Misc;
using Orso.Arpa.Tests.Shared.Extensions;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.UrlsTests.MappingProfileTests
{
    [TestFixture]
    public class UrlModifyCommandMappingProfileTests
    {
        protected DateTimeProviderContext _dateTimeProviderContext;

        [OneTimeSetUp]
        protected void OneTimeSetUp()
        {
            _dateTimeProviderContext = new DateTimeProviderContext(new DateTime(2021, 1, 1));
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            _dateTimeProviderContext.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Modify.MappingProfile>();
            });

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Entities.Url sourceUrl = FakeUrls.ArpaWebsite;
            Entities.Url expectedUrl = FakeUrls.ArpaWebsite;
            expectedUrl.SetProperty(nameof(Entities.Url.Href), expectedUrl.Href + "/modified");
            expectedUrl.SetProperty(nameof(Entities.Url.AnchorText), expectedUrl.AnchorText + " modified");
            var command = new Modify.Command
            {
                Id = expectedUrl.Id,
                Href = expectedUrl.Href,
                AnchorText = expectedUrl.AnchorText,
            };

            // Act
            Entities.Url url = _mapper.Map(command, sourceUrl);

            // Assert
            //Todo: tbd - UrlRoles needs to map, too!
            url.Should().BeEquivalentTo(expectedUrl, opt => opt.Excluding(dto => dto.UrlRoles));
        }
    }
}
