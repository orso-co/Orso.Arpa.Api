using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.MembershipHistoryApplication.Model;

namespace Orso.Arpa.Application.MembershipHistoryApplication.Interfaces
{
    public interface IMembershipHistoryService
    {
        Task<IEnumerable<MembershipHistoryDto>> GetByMembershipAsync(Guid membershipId);
        Task<MembershipHistoryDto> CreateAsync(MembershipHistoryCreateDto createDto);
        Task ModifyAsync(MembershipHistoryModifyDto modifyDto);
        Task DeleteAsync(Guid id);
    }
}
