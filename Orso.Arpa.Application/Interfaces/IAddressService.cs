using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.AddressApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IAddressService
    {
        Task<AddressDto> CreateAsync(AddressCreateDto addressCreateDto);
        Task ModifyAsync(AddressModifyDto addressModifyDto);
        Task DeleteAsync(Guid id);
    }
}
