using System;
using System.ComponentModel.DataAnnotations;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class PushSubscription : BaseEntity
    {
        public PushSubscription(Guid? id, Guid userId, string endpoint, string p256dh, string auth, string userAgent = null)
            : base(id)
        {
            UserId = userId;
            Endpoint = endpoint;
            P256dh = p256dh;
            Auth = auth;
            UserAgent = userAgent;
        }

        protected PushSubscription()
        {
        }

        public Guid UserId { get; private set; }

        [MaxLength(2048)]
        public string Endpoint { get; private set; }

        [MaxLength(512)]
        public string P256dh { get; private set; }

        [MaxLength(512)]
        public string Auth { get; private set; }

        [MaxLength(500)]
        public string UserAgent { get; private set; }

        public virtual User User { get; private set; }
    }
}
