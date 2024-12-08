using System;
using System.Text.Json.Serialization;
using AutoMapper;
using Orso.Arpa.Application.General.MappingActions;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class ReducedPersonDto
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string DisplayName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [IncludeForRoles(RoleNames.Staff)]
        public string UserEmail { get; set; }
    }

    public class ReducedPersonDtoMappingProfile : Profile
    {
        public ReducedPersonDtoMappingProfile()
        {
            CreateMap<Person, ReducedPersonDto>()
              .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty))
              .AfterMap<RoleBasedSetNullAction<Person, ReducedPersonDto>>();
        }
    }
}
