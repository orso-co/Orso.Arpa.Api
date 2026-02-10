using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.MembershipHistoryApplication.Interfaces;
using Orso.Arpa.Application.MembershipHistoryApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.MembershipHistoryApplication.Services
{
    public class MembershipHistoryService : BaseService<
        MembershipHistoryDto,
        MembershipHistory,
        MembershipHistoryCreateDto,
        CreateMembershipHistory.Command,
        MembershipHistoryModifyDto,
        MembershipHistoryModifyBodyDto,
        ModifyMembershipHistory.Command>, IMembershipHistoryService
    {
        public MembershipHistoryService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<MembershipHistoryDto>> GetByMembershipAsync(Guid membershipId)
        {
            return await GetAsync(
                predicate: h => h.PersonMembershipId == membershipId,
                orderBy: q => q.OrderByDescending(h => h.Year));
        }
    }
}
