using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.RoomApplication.Interfaces;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Application.RoomApplication.Services
{
    public class RoomService : BaseService<
        RoomDto,
        Room,
        RoomCreateDto,
        CreateRoom.Command,
        RoomModifyDto,
        RoomModifyBodyDto,
        ModifyRoom.Command>, IRoomService
    {
        public RoomService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}