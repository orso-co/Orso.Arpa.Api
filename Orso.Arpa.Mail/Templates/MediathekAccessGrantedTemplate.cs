namespace Orso.Arpa.Mail.Templates
{
    public class MediathekAccessGrantedTemplate : BaseTemplate
    {
        public override string Name => "Mediathek_Access_Granted";
        public string MediathekUrl { get; set; }
    }
}
