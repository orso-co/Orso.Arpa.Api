using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.PersonApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreateAsync(PersonCreateDto createDto);

        Task<PersonDto> GetByIdAsync(Guid id);

        Task<IEnumerable<PersonDto>> GetAsync();

        Task ModifyAsync(PersonModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}
