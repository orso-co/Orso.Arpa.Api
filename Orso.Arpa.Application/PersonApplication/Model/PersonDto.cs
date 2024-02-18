using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper;
using Orso.Arpa.Application.BankAccountApplication.Model;
using Orso.Arpa.Application.ContactDetailApplication.Model;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Application.PersonApplication.Model
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

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public string PersonBackgroundTeam { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public byte ExperienceLevel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public byte Reliability { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public byte GeneralPreference { get; set; }

        public IList<PersonAddressDto> Addresses { get; set; } = new List<PersonAddressDto>();

        public IList<SectionDto> StakeholderGroups { get; set; } = new List<SectionDto>();
    }

    public class PersonDtoMappingProfile : Profile
    {
        public PersonDtoMappingProfile()
        {
            _ = CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.StakeholderGroups, opt => opt.MapFrom(src => src.StakeholderGroups.Select(g => g.Section)))
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .AfterMap<RoleBasedSetNullAction<Person, PersonDto>>();
        }
    }
}
