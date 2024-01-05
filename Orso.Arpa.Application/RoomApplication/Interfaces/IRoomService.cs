using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.RoomApplication.Model;

namespace Orso.Arpa.Application.RoomApplication.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> GetByIdAsync(Guid id);

        Task<RoomDto> CreateAsync(RoomCreateDto createDto);

        Task ModifyAsync(RoomModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}