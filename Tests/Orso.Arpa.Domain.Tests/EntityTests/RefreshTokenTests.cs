using System;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Domain.Tests.EntityTests
{
    [TestFixture]
    public class RefreshTokenTests
    {
        [Test]
        public void Should_Be_Expired()
        {
            // Arrange
            var refreshToken = new RefreshToken("token", DateTime.MinValue, "ip", Guid.Empty, FakeDateTime.UtcNow);

            // Assert
            refreshToken.IsExpired(FakeDateTime.UtcNow).Should().BeTrue();
            refreshToken.IsActive(FakeDateTime.UtcNow).Should().BeFalse();
        }

        [Test]
        public void Should_Not_Be_Expired()
        {
            // Arrange
            var refreshToken = new RefreshToken("token", DateTime.MaxValue, "ip", Guid.Empty, FakeDateTime.UtcNow);

            // Assert
            refreshToken.IsExpired(FakeDateTime.UtcNow).Should().BeFalse();
            refreshToken.IsActive(FakeDateTime.UtcNow).Should().BeTrue();
        }

        [Test]
        public void Should_Revoke()
        {
            // Arrange
            var refreshToken = new RefreshToken("token", DateTime.MaxValue, "ip", Guid.Empty, FakeDateTime.UtcNow);
            var command = new RevokeRefreshToken.Command() { RefreshToken = "token", RemoteIpAddress = "ip2" };

            // Act
            refreshToken.Revoke(command, FakeDateTime.UtcNow);

            // Assert
            refreshToken.RevokedByIp.Should().BeEquivalentTo("ip2");
            refreshToken.RevokedOn.Should().BeMoreThan(TimeSpan.Zero);
            refreshToken.IsActive(FakeDateTime.UtcNow).Should().BeFalse();
            refreshToken.IsExpired(FakeDateTime.UtcNow).Should().BeFalse();
        }
    }
}
