using System;

namespace Orso.Arpa.Domain.AddressDomain.Interfaces
{
    public class BaseAddressCreateCommand
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string UrbanDistrict { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string CommentInner { get; set; }
        public Guid? TypeId { get; set; }
    }
}
