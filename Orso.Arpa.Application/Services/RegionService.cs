using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Regions;

namespace Orso.Arpa.Application.Services
{
    public class RegionService : BaseService<
        RegionDto,
        Region,
        RegionCreateDto,
        Create.Command,
        RegionModifyDto,
        RegionModifyBodyDto,
        Modify.Command>, IRegionService
    {
        public RegionService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
