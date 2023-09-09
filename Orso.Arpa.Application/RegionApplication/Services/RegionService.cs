using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.RegionApplication.Interfaces;
using Orso.Arpa.Application.RegionApplication.Model;
using Orso.Arpa.Domain.RegionDomain.Commands;
using Orso.Arpa.Domain.RegionDomain.Enums;
using Orso.Arpa.Domain.RegionDomain.Model;

namespace Orso.Arpa.Application.RegionApplication.Services
{
    public class RegionService : BaseService<
        RegionDto,
        Region,
        RegionCreateDto,
        CreateRegion.Command,
        RegionModifyDto,
        RegionModifyBodyDto,
        ModifyRegion.Command>, IRegionService
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
