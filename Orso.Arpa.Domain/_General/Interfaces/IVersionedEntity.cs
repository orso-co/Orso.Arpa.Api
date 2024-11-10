using System;
namespace Orso.Arpa.Domain._General.Interfaces
{
    public interface IVersionedEntity
    {
        DateTime ValidFrom { get; }
    }
}
