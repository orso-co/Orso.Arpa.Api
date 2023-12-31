using Orso.Arpa.Mail.Interfaces;

namespace Orso.Arpa.Mail.Templates
{
    public class ConfirmEmailTemplate : BaseTemplate
    {
        public string ClientUri { get; set; }
        public override string Name => "Confirm_Email";
    }
}
