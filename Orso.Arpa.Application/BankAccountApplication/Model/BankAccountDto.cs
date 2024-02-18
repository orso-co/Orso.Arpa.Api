using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.BankAccountApplication.Model
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
            CreateMap<PersonBankAccount, BankAccountDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
