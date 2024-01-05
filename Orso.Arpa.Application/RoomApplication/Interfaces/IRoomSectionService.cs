using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.RoomApplication.Model;

namespace Orso.Arpa.Application.RoomApplication.Interfaces
{
    public interface IRoomSectionService
    {
        Task<RoomSectionDto> GetByIdAsync(Guid id);

        Task<RoomSectionDto> CreateAsync(RoomSectionCreateDto createDto);

        Task ModifyAsync(RoomSectionModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}