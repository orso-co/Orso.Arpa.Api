namespace Orso.Arpa.Domain.Configuration
{
    public class JwtConfiguration
    {
        public string TokenKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInDays { get; set; }
    }
}
