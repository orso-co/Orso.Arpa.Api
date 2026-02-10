using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.PersonMembershipApplication.Interfaces;
using Orso.Arpa.Application.PersonMembershipApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
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
        private readonly IArpaContext _arpaContext;

        public PersonMembershipService(IMediator mediator, IMapper mapper, IArpaContext arpaContext) : base(mediator, mapper)
        {
            _arpaContext = arpaContext;
        }

        public async Task<IEnumerable<PersonMembershipDto>> GetByPersonAsync(Guid personId)
        {
            var memberships = await _arpaContext.Set<PersonMembership>()
                .Where(pm => pm.PersonId == personId && !pm.Deleted)
                .Include(pm => pm.MembershipHistories.Where(h => !h.Deleted))
                .OrderByDescending(pm => pm.EntryDate)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PersonMembershipDto>>(memberships);
        }
    }
}
