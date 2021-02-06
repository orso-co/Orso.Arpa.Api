using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail.Templates
{
    public class ActivationInfoTemplate : ITemplate
    {
        public string ArpaLogo { get; set; }
        public string DisplayName { get; set; }
        public string ClubName { get; set; }
        public string ClubMail { get; set; }
        public string ClubAddress { get; set; }
        public string ClubPhoneNumber { get; set; }
        public string Name => "Activation_Info";
    }
}
