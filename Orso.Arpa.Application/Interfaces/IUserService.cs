using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.UserApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAsync();

        Task<UserDto> GetByIdAsync(Guid id);

        Task DeleteAsync(string userName);
    }
}
