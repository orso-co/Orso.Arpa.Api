using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.MembershipHistoryApplication.Model
{
    public class MembershipHistoryDto : BaseEntityDto
    {
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public bool IsReduced { get; set; }
        public string Comment { get; set; }
    }

    public class MembershipHistoryDtoMappingProfile : Profile
    {
        public MembershipHistoryDtoMappingProfile()
        {
            CreateMap<MembershipHistory, MembershipHistoryDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
