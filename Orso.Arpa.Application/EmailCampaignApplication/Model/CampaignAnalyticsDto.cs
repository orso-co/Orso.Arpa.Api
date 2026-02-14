namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class CampaignAnalyticsDto
{
    public int TotalRecipients { get; set; }
    public int SentCount { get; set; }
    public int FailedCount { get; set; }
    public int OpenedCount { get; set; }
    public int PendingCount { get; set; }
    public double OpenRate { get; set; }
}
