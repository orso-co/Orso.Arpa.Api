namespace Orso.Arpa.Mail.Templates
{
    public class AppointmentChangedByStaffTemplate : BaseTemplate
    {
        public override string Name => "Appointment_Changed_By_Staff";
        public string AppointmentName { get; set; }
        public string DateAndTime { get; set; }
        public string PublicDetails { get; set; }
        public string Venue { get; set; }
        public string ArpaUrl { get; set; }
        public string Sections { get; set; }
        public string Status { get; set; }
    }
}
