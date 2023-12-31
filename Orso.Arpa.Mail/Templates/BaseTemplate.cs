using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail.Templates
{
    public abstract class BaseTemplate : ITemplate
    {
        public abstract string Name { get; }
        public string ArpaLogo { get; set; }
        public string DisplayName { get; set; }
        public string ClubName { get; set; }
        public string ClubMail { get; set; }
        public string ClubAddress { get; set; }
        public string ClubPhoneNumber { get; set; }
    }
}