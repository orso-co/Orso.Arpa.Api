using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.PersonMembershipApplication.Interfaces;
using Orso.Arpa.Application.PersonMembershipApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.PersonMembershipApplication.Services
{
    public class PersonMembershipService : BaseService<
        PersonMembershipDto,
        PersonMembership,
        PersonMembershipCreateDto,
        CreatePersonMembership.Command,
        PersonMembershipModifyDto,
        PersonMembershipModifyBodyDto,
        ModifyPersonMembership.Command>, IPersonMembershipService
    {
        public PersonMembershipService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<PersonMembershipDto>> GetByPersonAsync(Guid personId)
        {
            return await GetAsync(predicate: pm => pm.PersonId == personId);
        }
    }
}
