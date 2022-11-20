namespace Orso.Arpa.Domain.Enums
{
    /// <summary>
    /// AppointmentParticipationResult
    /// </summary>
    /// <remarks>Max key length 100</remarks>
    public enum AppointmentParticipationResult
    {
        Present = 10,
        Absent = 20,
        Inapplicable = 30,
        Ambiguous = 40,
        AwaitingScan = 50
    }
}
