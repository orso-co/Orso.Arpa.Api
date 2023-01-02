using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.BankAccountApplication;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Application.PersonApplication
{
    public class PersonDto : BaseEntityDto
    {
        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string AboutMe { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public ReducedPersonDto ContactVia { get; set; }

        public IList<ReducedPersonDto> ContactsRecommended { get; set; } = new List<ReducedPersonDto>();

        public IList<BankAccountDto> BankAccounts { get; set; } = new List<BankAccountDto>();

        public IList<ContactDetailDto> ContactDetails { get; set; } = new List<ContactDetailDto>();

        public SelectValueDto Gender { get; set; }

        public string BirthName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Birthplace { get; set; }

        [IncludeForRoles(RoleNames.Staff)]
        public string Background { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public byte ExperienceLevel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public byte Reliability { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public byte GeneralPreference { get; set; }

        public IList<AddressDto> Addresses { get; set; } = new List<AddressDto>();

        public IList<SectionDto> StakeholderGroups { get; set; } = new List<SectionDto>();
    }

    public class PersonDtoMappingProfile : Profile
    {
        public PersonDtoMappingProfile()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.StakeholderGroups, opt => opt.MapFrom(src => src.StakeholderGroups.Select(g => g.Section)))
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .AfterMap<RoleBasedSetNullAction<Person, PersonDto>>();
        }
    }
}
