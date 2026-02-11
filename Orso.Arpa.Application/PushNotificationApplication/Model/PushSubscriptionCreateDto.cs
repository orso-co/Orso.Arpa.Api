using System.ComponentModel.DataAnnotations;

namespace Orso.Arpa.Application.PushNotificationApplication.Model
{
    public class PushSubscriptionCreateDto
    {
        [Required]
        [MaxLength(2048)]
        public string Endpoint { get; set; }

        [Required]
        [MaxLength(512)]
        public string P256dh { get; set; }

        [Required]
        [MaxLength(512)]
        public string Auth { get; set; }

        [MaxLength(500)]
        public string UserAgent { get; set; }
    }
}
