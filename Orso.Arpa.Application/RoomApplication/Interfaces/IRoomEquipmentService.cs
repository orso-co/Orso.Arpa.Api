using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.RoomApplication.Model;

namespace Orso.Arpa.Application.RoomApplication.Interfaces
{
    public interface IRoomEquipmentService
    {
        Task<RoomEquipmentDto> GetByIdAsync(Guid id);

        Task<RoomEquipmentDto> CreateAsync(RoomEquipmentCreateDto createDto);

        Task ModifyAsync(RoomEquipmentModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}