using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.RegionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
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

        public async Task<IEnumerable<RegionDto>> GetAsync(RegionPreferenceType regionPreferenceType)
        {
            switch (regionPreferenceType)
            {
                case RegionPreferenceType.None:
                    return await GetAsync();
                case RegionPreferenceType.Rehearsal:
                    return await GetAsync(r => r.IsForRehearsal);
                case RegionPreferenceType.Performance:
                    return await GetAsync(r => r.IsForPerformance);
                default:
                    throw new NotSupportedException($"The region preference type '{regionPreferenceType}' is not supported");
            }
        }
    }
}
