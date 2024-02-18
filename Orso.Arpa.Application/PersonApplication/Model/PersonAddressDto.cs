using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class PersonAddressDto : BaseEntityDto
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

    public class PersonAddressDtoMappingProfile : Profile
    {
        public PersonAddressDtoMappingProfile()
        {
            CreateMap<PersonAddress, PersonAddressDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
