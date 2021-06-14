using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Logic.Sections;

namespace Orso.Arpa.Application.Services
{
    public class SectionService : BaseService<SectionDto, Section, SectionCreateDto, Create.Command, SectionModifyDto, SectionModifyBodyDto, Modify.Command>, ISectionService
    {
        public SectionService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<SectionDto>> GetAsync(bool instrumentsOnly)
        {
            Expression<Func<Section, bool>> predicate = (section) => section.IsInstrument;
            return await base.GetAsync(orderBy: s => s.OrderBy(s => s.Name), predicate: instrumentsOnly ? predicate : null);
        }

        public async Task<IEnumerable<SectionDto>> GetDoublingInstrumentsAsync(Guid id)
        {
            var query = new DoublingInstruments.Query { Id = id };
            IEnumerable<Section> doublingInstruments = await _mediator.Send(query);
            return _mapper.Map<IEnumerable<SectionDto>>(doublingInstruments);
        }

        public async Task<SectionTreeDto> GetTreeAsync(int? maxLevel)
        {
            var query = new Tree.Query() { MaxLevel = maxLevel };
            ITree<Section> tree = await _mediator.Send(query);
            return _mapper.Map<SectionTreeDto>(tree);
        }
    }
}
