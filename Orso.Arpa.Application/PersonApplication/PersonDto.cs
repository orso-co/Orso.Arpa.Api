using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AutoMapper;
using Orso.Arpa.Application.AddressApplication;
using Orso.Arpa.Application.BankAccountApplication;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.General;
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
        public byte ExperienceLevel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public byte Reliability { get; set; }

        public IList<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    }

    public class PersonDtoMappingProfile : Profile
    {
        public PersonDtoMappingProfile()
        {
            CreateMap<Person, PersonDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
