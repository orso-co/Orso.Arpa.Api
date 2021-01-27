using System;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Domain.Tests.EntityTests
{
    [TestFixture]
    public class RefreshTokenTests
    {
        [Test]
        public void Should_Be_Expired()
        {
            // Arrange
            var refreshToken = new RefreshToken("token", DateTime.MinValue, "ip", Guid.Empty);

            // Assert
            refreshToken.IsExpired.Should().BeTrue();
            refreshToken.IsActive.Should().BeFalse();
        }

        [Test]
        public void Should_Not_Be_Expired()
        {
            // Arrange
            var refreshToken = new RefreshToken("token", DateTime.MaxValue, "ip", Guid.Empty);

            // Assert
            refreshToken.IsExpired.Should().BeFalse();
            refreshToken.IsActive.Should().BeTrue();
        }

        [Test]
        public void Should_Revoke()
        {
            // Arrange
            var refreshToken = new RefreshToken("token", DateTime.MaxValue, "ip", Guid.Empty);
            var command = new RevokeRefreshToken.Command() { RefreshToken = "token", RemoteIpAddress = "ip2" };

            // Act
            refreshToken.Revoke(command);

            // Assert
            refreshToken.RevokedByIp.Should().BeEquivalentTo("ip2");
            refreshToken.RevokedOn.Should().BeMoreThan(TimeSpan.Zero);
            refreshToken.IsActive.Should().BeFalse();
            refreshToken.IsExpired.Should().BeFalse();
        }
    }
}
