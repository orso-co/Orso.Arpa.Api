using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class PersonInviteResultDto
    {
        public IDictionary<string, string> SuccessfulInvites { get; set; } = new Dictionary<string, string>();

        public IList<string> PersonsWithoutEmailAddress { get; set; } = new List<string>();
        public IList<string> PersonsAlreadyRegistered { get; set; } = new List<string>();
        public IDictionary<string, IList<string>> PersonsWithMultipleEmailAddresses { get; set; } = new Dictionary<string, IList<string>>();
        public IDictionary<string, string> FailedInvites { get; set; } = new Dictionary<string, string>();
    }

    public class PersonInviteResultDtoMappingProfile : Profile
    {
        public PersonInviteResultDtoMappingProfile()
        {
            CreateMap<PersonInviteResult, PersonInviteResultDto>();
        }
    }
}
