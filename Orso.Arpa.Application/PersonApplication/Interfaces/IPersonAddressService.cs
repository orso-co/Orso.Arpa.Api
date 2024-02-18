using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.PersonApplication.Model;

namespace Orso.Arpa.Application.PersonApplication.Interfaces
{
    public interface IPersonAddressService
    {
        Task<PersonAddressDto> CreateAsync(PersonAddressCreateDto addressCreateDto);
        Task ModifyAsync(PersonAddressModifyDto addressModifyDto);
        Task DeleteAsync(Guid id);
    }
}
