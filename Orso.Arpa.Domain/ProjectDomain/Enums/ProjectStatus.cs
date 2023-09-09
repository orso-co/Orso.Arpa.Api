namespace Orso.Arpa.Domain.ProjectDomain.Enums
{
    /// <summary>
    /// ProjectStatus
    /// </summary>
    /// <remarks>Max key length 100</remarks>
    public enum ProjectStatus
    {
        Pending = 10,
        Confirmed = 20,
        Cancelled = 30,
        Postponed = 40,
        Archived = 50
    }
}
