using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail.Templates
{
    public class SupportTicketConfirmationTemplate : ITemplate
    {
        public string ArpaLogo { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string TopicsFormatted { get; set; }
        public string ClubName { get; set; }
        public string ClubMail { get; set; }
        public string ClubAddress { get; set; }
        public string ClubPhoneNumber { get; set; }

        public string Name => "Support_Ticket_Confirmation";
    }
}
