using AutoMapper;
using Orso.Arpa.Application.AddressApplication.Model;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Application.VenueApplication.Model
{
    public class VenueListDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; }
    }

    public class VenueListDtoMappingProfile : Profile
    {
        public VenueListDtoMappingProfile()
        {
            CreateMap<Venue, VenueListDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
