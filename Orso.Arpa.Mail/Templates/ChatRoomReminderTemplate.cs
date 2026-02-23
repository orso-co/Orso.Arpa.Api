namespace Orso.Arpa.Mail.Templates
{
    public class ChatRoomReminderTemplate : BaseTemplate
    {
        public override string Name => "Chat_Room_Reminder";
        public string RoomName { get; set; }
        public string SenderName { get; set; }
        public string LastMessagePreview { get; set; }
        public string ChatLink { get; set; }
    }
}
