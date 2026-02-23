namespace Orso.Arpa.Domain.General.Configuration
{
    public class FinanceConfiguration
    {
        public string SyncScheduleCron { get; set; } = "0 6 * * *";
        public int SyncIntervalMinutes { get; set; } = 360;
        public string NtfyTopic { get; set; } = "WolfsClaude";
    }
}
