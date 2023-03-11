using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        private IArpaContext _arpaContext;
        private Domain.Logic.SelectValues.List.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _arpaContext = Substitute.For<IArpaContext>();
            _handler = new Domain.Logic.SelectValues.List.Handler(_arpaContext);
        }

        [Test]
        public async Task Should_Get_List()
        {
            // Arrange
            IList<SelectValueCategory> categories = SelectValueCategorySeedData.SelectValueCategories;
            foreach (SelectValueCategory category in categories)
            {
                foreach (SelectValueMapping mapping in SelectValueMappingSeedData.SelectValueMappings
                    .Where(svm => svm.SelectValueCategoryId == category.Id))
                {
                    mapping.SetProperty(
                        nameof(SelectValueMapping.SelectValue),
                        SelectValueSeedData.SelectValues.FirstOrDefault(sv => sv.Id == mapping.SelectValueId));
                    category.SelectValueMappings.Add(mapping);
                }
            }
            ICollection<SelectValueMapping> expectedMappings = categories
                .FirstOrDefault(c => c.Table == nameof(Address) && c.Property == nameof(Address.Type))?
                .SelectValueMappings;
            DbSet<SelectValueCategory> categoriesToReturn = categories.AsQueryable().BuildMockDbSet();
            _ = categoriesToReturn.AsQueryable().Returns(categories.AsQueryable());
            _ = _arpaContext.SelectValueCategories.Returns(categoriesToReturn);

            // Act
            IImmutableList<SelectValueMapping> result = await _handler.Handle(
                new Domain.Logic.SelectValues.List.Query
                {
                    TableName = nameof(Address),
                    PropertyName = nameof(Address.Type)
                },
                new CancellationToken());

            // Assert
            _ = result.Should().BeEquivalentTo(expectedMappings);
        }
    }
}
