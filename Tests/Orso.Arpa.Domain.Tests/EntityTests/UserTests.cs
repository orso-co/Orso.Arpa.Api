using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.EntityTests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Should_Set_Deletion_Flag()
        {
            // Arrange
            User user = FakeUsers.Orsianer;

            // Act
            user.Delete();

            // Assert
            user.Deleted.Should().BeTrue();
        }
    }
}
