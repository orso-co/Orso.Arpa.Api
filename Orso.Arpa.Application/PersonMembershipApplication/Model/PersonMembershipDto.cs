using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.PersonMembershipApplication.Model
{
    public class PersonMembershipDto : BaseEntityDto
    {
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public decimal AnnualFee { get; set; }
        public SelectValueDto SupportLevel { get; set; }
        public SelectValueDto MembershipStatus { get; set; }
        public SelectValueDto PaymentMethod { get; set; }
        public SelectValueDto PaymentFrequency { get; set; }
        public SelectValueDto Club { get; set; }
        public string StaffComment { get; set; }
        public string PerformerComment { get; set; }
    }

    public class PersonMembershipDtoMappingProfile : Profile
    {
        public PersonMembershipDtoMappingProfile()
        {
            CreateMap<PersonMembership, PersonMembershipDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
