using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Domain.Entities
{
    public class RefreshToken
    {
        public RefreshToken(string token, DateTime expiryOn, string createdByIp, Guid userId)
        {
            Token = token;
            ExpiryOn = expiryOn;
            CreatedByIp = createdByIp;
            UserId = userId;
        }

        [JsonConstructor]
        protected RefreshToken()
        {
        }

        public void Revoke(RevokeRefreshToken.Command command)
        {
            RevokedByIp = command.RemoteIpAddress;
            RevokedOn = DateTimeProvider.Instance.GetUtcNow();
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
        public DateTime CreatedOn { get; private set; } = DateTimeProvider.Instance.GetUtcNow();

        [JsonInclude]
        public string CreatedByIp { get; private set; }

        [JsonInclude]
        public DateTime RevokedOn { get; private set; }

        [JsonInclude]
        public string RevokedByIp { get; private set; }

        [JsonInclude]
        public bool IsExpired => DateTimeProvider.Instance.GetUtcNow() >= ExpiryOn;

        [JsonInclude]
        public bool IsActive => RevokedByIp == null && RevokedOn == DateTime.MinValue && !IsExpired;
    }
}
