using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.UserApplication.Model;

namespace Orso.Arpa.Application.UserApplication.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAsync();

        Task<UserDto> GetByIdAsync(Guid id);

        Task DeleteAsync(string userName);
    }
}
