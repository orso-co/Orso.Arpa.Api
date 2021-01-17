namespace Orso.Arpa.Infrastructure.Authentication
{
    public class JwtConfiguration
    {
        public string TokenKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationTimeInMinutes { get; set; }
    }
}
