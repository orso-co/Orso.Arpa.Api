using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Application.SectionApplication.Interfaces;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.SectionDomain.Commands;
using Orso.Arpa.Domain.SectionDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Queries;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Application.SectionApplication.Services
{
    public class SectionService : BaseService<
        SectionDto,
        Section,
        SectionCreateDto,
        CreateSection.Command,
        SectionModifyDto,
        SectionModifyBodyDto,
        ModifySection.Command>, ISectionService
    {
        public SectionService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<SectionDto>> GetAsync(bool instrumentsWithChildrenOnly)
        {
            if (instrumentsWithChildrenOnly)
            {
                var query = new InstrumentsWithChildren.Query();
                IEnumerable<Section> instruments = await _mediator.Send(query);
                return _mapper.Map<IEnumerable<SectionDto>>(instruments);
            }
            return await base.GetAsync();
        }

        public async Task<IEnumerable<SectionDto>> GetDoublingInstrumentsAsync(Guid id)
        {
            var query = new DoublingInstruments.Query { Id = id };
            IEnumerable<Section> doublingInstruments = await _mediator.Send(query);
            return _mapper.Map<IEnumerable<SectionDto>>(doublingInstruments);
        }

        public async Task<IEnumerable<SelectValueDto>> GetPositionsAsync(Guid id)
        {
            var query = new Positions.Query { Id = id };
            IQueryable<SelectValueSection> positions = await _mediator.Send(query);

            List<SelectValueSection> list = await positions.ToListAsync();
            return _mapper.Map<IEnumerable<SelectValueDto>>(list);
        }

        public async Task<SectionTreeDto> GetTreeAsync(int? maxLevel)
        {
            var query = new Tree.Query() { MaxLevel = maxLevel };
            ITree<Section> tree = await _mediator.Send(query);
            return _mapper.Map<SectionTreeDto>(tree);
        }
    }
}
