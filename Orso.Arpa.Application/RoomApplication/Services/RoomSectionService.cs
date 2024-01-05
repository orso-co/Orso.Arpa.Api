using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.RoomApplication.Interfaces;
using Orso.Arpa.Application.RoomApplication.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;
using Orso.Arpa.Domain.VenueDomain.Model;

namespace Orso.Arpa.Application.RoomApplication.Services
{
    public class RoomSectionService: BaseService<
        RoomSectionDto,
        RoomSection,
        RoomSectionCreateDto,
        CreateRoomSection.Command,
        RoomSectionModifyDto,
        RoomSectionModifyBodyDto,
        ModifyRoomSection.Command>, IRoomSectionService
    {
        public RoomSectionService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}