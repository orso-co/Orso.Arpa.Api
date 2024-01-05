using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.RoomApplication.Interfaces;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Application.RoomApplication.Services
{
    public class RoomEquipmentService: BaseService<
        RoomEquipmentDto,
        RoomEquipment,
        RoomEquipmentCreateDto,
        CreateRoomEquipment.Command,
        RoomEquipmentModifyDto,
        RoomEquipmentModifyBodyDto,
        ModifyRoomEquipment.Command>, IRoomEquipmentService
    {
        public RoomEquipmentService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}