namespace Orso.Arpa.Domain.Interfaces
{
    public interface ITokenAccessor
    {
        string UserName { get; }
        string DisplayName { get; }
        string UserRole { get; }
    }
}
