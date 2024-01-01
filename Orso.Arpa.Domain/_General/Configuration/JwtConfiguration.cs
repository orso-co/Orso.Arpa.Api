namespace Orso.Arpa.Domain.General.Configuration
{
    public class JwtConfiguration
    {
        public string TokenKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInDays { get; set; }

        public string ArpaLogo => $"{Audience}/assets/common/logos/arpa_logo.png";
    }
}
