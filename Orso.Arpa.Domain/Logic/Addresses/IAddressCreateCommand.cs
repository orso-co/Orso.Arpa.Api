using System;

namespace Orso.Arpa.Domain.Logic.Addresses
{
    public interface IAddressCreateCommand
    {
        string Address1 { get; }
        string Address2 { get; }
        string Zip { get; }
        string City { get; }
        string UrbanDistrict { get; }
        string Country { get; }
        string State { get; }
        string CommentInner { get; }
        Guid? TypeId { get; }
    }
}
