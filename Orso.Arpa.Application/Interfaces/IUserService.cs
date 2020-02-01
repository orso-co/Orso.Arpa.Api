using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Application.Logic.Me;
using Orso.Arpa.Application.Logic.Users;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAsync();

        Task DeleteAsync(string userName);

        Task<UserProfileDto> GetProfileOfCurrentUserAsync();

        Task ModifyProfileOfCurrentUserAsync(Modify.Dto userProfileModifyDto);
    }
}
