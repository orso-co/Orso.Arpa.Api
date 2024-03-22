namespace Orso.Arpa.Domain.General.Configuration
{
    public class IdentityConfiguration
    {
        public int LockoutExpiryInMinutes { get; set; }
        public int MaxFailedLoginAttempts { get; set; }
        public int EmailConfirmationTokenExpiryInDays { get; set; }
        public int DataProtectionTokenExpiryInHours { get; set; }
    }
}
