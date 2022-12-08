using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail.Templates
{
    public class MusicianProfileCreatedTemplate : ITemplate
    {
        public string Name => "Musician_Profile_Created";
        public string DisplayName { get; set; }
        public string Instrument { get; set; }
        public string Qualification { get; set; }
        public string LevelAssessmentInner { get; set; }
        public string ArpaLogo { get; set; }
        public string Id { get; set; }

        public string ClubName { get; set; }

        public string ClubMail { get; set; }
        public string ClubAddress { get; set; }
        public string ClubPhoneNumber { get; set; }
    }
}
