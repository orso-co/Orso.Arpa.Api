using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    [AuditLogIgnore]
    public class RefreshToken
    {
        public RefreshToken(string token, DateTime expiryOn, string createdByIp, Guid userId, DateTime createdAt)
        {
            Token = token;
            ExpiryOn = expiryOn;
            CreatedByIp = createdByIp;
            UserId = userId;
            CreatedOn = createdAt;
        }

        [JsonConstructor]
        protected RefreshToken()
        {
        }

        public void Revoke(RevokeRefreshToken.Command command, DateTime revokedAt)
        {
            RevokedByIp = command.RemoteIpAddress;
            RevokedOn = revokedAt;
        }

        [JsonInclude]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [JsonInclude]
        public string Token { get; private set; }

        [JsonInclude]
        public Guid UserId { get; private set; }

        [JsonInclude]
        public virtual User User { get; private set; }

        [JsonInclude]
        public DateTime ExpiryOn { get; private set; }

        [JsonInclude]
        public DateTime CreatedOn { get; private set; }

        [JsonInclude]
        public string CreatedByIp { get; private set; }

        [JsonInclude]
        public DateTime RevokedOn { get; private set; }

        [JsonInclude]
        public string RevokedByIp { get; private set; }

        public bool IsExpired(DateTime utcNow) => utcNow >= ExpiryOn;

        public bool IsActive(DateTime utcNow) => RevokedByIp == null && RevokedOn == DateTime.MinValue && !IsExpired(utcNow);
    }
}
