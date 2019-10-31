using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Domain.Tests
{
    [TestFixture]
    public class UserTests
    {

        [Test]
        public void Should_Set_Deletion_Flag()
        {
            // Arrange
            User user = UserSeedData.Egon;

            // Act
            user.Delete();

            // Assert
            user.Deleted.Should().BeTrue();
        }
    }
}
