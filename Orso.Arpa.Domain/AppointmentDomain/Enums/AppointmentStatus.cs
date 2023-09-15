namespace Orso.Arpa.Domain.AppointmentDomain.Enums
{
    /// <summary>
    /// AppointmentStatus
    /// </summary>
    /// <remarks>Max key length 100</remarks>
    public enum AppointmentStatus
    {
        Confirmed = 10,
        Scheduled = 20,
        Ambiguous = 30,
        AwaitingPoll = 40,
        Refused = 50
    }
}
