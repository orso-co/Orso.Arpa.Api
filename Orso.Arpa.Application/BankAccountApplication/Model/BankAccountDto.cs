using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.BankAccountApplication
{
    public class BankAccountDto : BaseEntityDto
    {
        public string Iban { get; set; }
        public string Bic { get; set; }
        public SelectValueDto Status { get; set; }
        public string CommentInner { get; set; }
        public string AccountOwner { get; set; }
    }

    public class BankAccountDtoMappingProfile : Profile
    {
        public BankAccountDtoMappingProfile()
        {
            CreateMap<BankAccount, BankAccountDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
