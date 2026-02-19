using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.ChatDomain.Model
{
    public class ChatLiveLocationShare : BaseEntity
    {
        public ChatLiveLocationShare(Guid? id, Guid chatRoomId, Guid userId, Guid messageId,
            double latitude, double longitude, double? accuracy,
            DateTime expiresAt) : base(id)
        {
            ChatRoomId = chatRoomId;
            UserId = userId;
            MessageId = messageId;
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
            StartedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            LastUpdatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        [JsonConstructor]
        protected ChatLiveLocationShare()
        {
        }

        public void UpdatePosition(double latitude, double longitude, double? accuracy)
        {
            if (!IsActive || IsExpired())
            {
                Stop();
                return;
            }
            Latitude = latitude;
            Longitude = longitude;
            Accuracy = accuracy;
            LastUpdatedAt = DateTime.UtcNow;
        }

        public void Stop()
        {
            IsActive = false;
        }

        public bool IsExpired() => DateTime.UtcNow >= ExpiresAt;

        public Guid ChatRoomId { get; private set; }
        public virtual ChatRoom ChatRoom { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public Guid MessageId { get; private set; }
        public virtual ChatMessage Message { get; private set; }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public double? Accuracy { get; private set; }

        public DateTime StartedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }
        public bool IsActive { get; private set; }
    }
}
