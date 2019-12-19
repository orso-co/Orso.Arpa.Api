using System;

namespace Orso.Arpa.Application.Dtos
{
    public class AddressDto : BaseEntityDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string UrbanDistrict { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public Guid RegionId { get; set; }
    }
}
