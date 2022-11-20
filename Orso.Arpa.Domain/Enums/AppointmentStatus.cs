namespace Orso.Arpa.Domain.Enums
{
    /// <summary>
    /// AppointmentStatus
    /// </summary>
    /// <remarks>Max key length 100</remarks>
    public enum AppointmentStatus
    {
        Confirmed = 10,
        Scheduled = 20,
        Ambigous = 30,
        AwaitingPoll = 40,
        Refused = 50
    }
}
