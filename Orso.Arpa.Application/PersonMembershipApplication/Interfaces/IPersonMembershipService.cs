using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.PersonMembershipApplication.Model;

namespace Orso.Arpa.Application.PersonMembershipApplication.Interfaces
{
    public interface IPersonMembershipService
    {
        Task<IEnumerable<PersonMembershipDto>> GetByPersonAsync(Guid personId);
        Task<PersonMembershipDto> CreateAsync(PersonMembershipCreateDto createDto);
        Task ModifyAsync(PersonMembershipModifyDto modifyDto);
        Task DeleteAsync(Guid id);
    }
}
