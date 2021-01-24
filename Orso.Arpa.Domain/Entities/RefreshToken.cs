using System;
using Newtonsoft.Json;

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

        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Token { get; private set; }

        public Guid UserId { get; private set; }

        public virtual User User { get; private set; }

        public DateTime ExpiryOn { get; private set; }

        public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

        public string CreatedByIp { get; private set; }

        public DateTime RevokedOn { get; private set; }

        public string RevokedByIp { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiryOn;

        public bool IsActive => RevokedByIp == null && RevokedOn == DateTime.MinValue && !IsExpired;
    }
}
