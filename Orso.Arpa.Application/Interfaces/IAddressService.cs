using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.AddressApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAddressService
    {
        Task<AddressDto> CreateAsync(PersonAddressCreateDto addressCreateDto);
        Task ModifyAsync(PersonAddressModifyDto addressModifyDto);
        Task DeleteAsync(Guid id);
    }
}
