using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Domain.Logic.SelectValueCategories;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;

namespace Orso.Arpa.Domain.Tests.SelectValuesTests.QueryHandlerTests
{
    [TestFixture]
    public class ListHandlerTests
    {
        private IReadOnlyRepository _repository;
        private Logic.SelectValues.List.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IReadOnlyRepository>();
            _handler = new Logic.SelectValues.List.Handler(_repository);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            IList<SelectValueCategory> categories = SelectValueCategorySeedData.SelectValueCategories;
            foreach (SelectValueCategory category in categories)
            {
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.SelectValueMappings.Where(svm => svm.SelectValueCategoryId == category.Id))
                {
                    mapping.SetProperty(
                        nameof(SelectValueMapping.SelectValue),
                        SelectValueSeedData.SelectValues.FirstOrDefault(sv => sv.Id == mapping.SelectValueId));
                    category.SelectValueMappings.Add(mapping);
                }
            }
            ICollection<SelectValueMapping> expectedMappings = categories
                .FirstOrDefault(c => c.Table == nameof(PersonAddress) && c.Property == nameof(PersonAddress.Type))?
                .SelectValueMappings;
            IQueryable<SelectValueCategory> categoriesToReturn = categories.AsQueryable().BuildMock();
            _repository.GetAll<SelectValueCategory>()
                .Returns(categoriesToReturn);

            // Act
            IImmutableList<SelectValueMapping> result = await _handler.Handle(
                new Logic.SelectValues.List.Query
                {
                    TableName = nameof(PersonAddress),
                    PropertyName = nameof(PersonAddress.Type)
                },
                new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(expectedMappings);
        }
    }
}
