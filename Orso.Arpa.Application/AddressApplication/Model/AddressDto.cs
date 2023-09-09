using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Application.AddressApplication.Model
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
        public string CommentInner { get; set; }
        public SelectValueDto Type { get; set; }
    }

    public class AddressDtoMappingProfile : Profile
    {
        public AddressDtoMappingProfile()
        {
            CreateMap<Address, AddressDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
