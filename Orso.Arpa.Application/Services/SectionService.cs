using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.SectionApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Sections;

namespace Orso.Arpa.Application.Services
{
    public class SectionService : BaseService<SectionDto, Section, SectionCreateDto, Create.Command, SectionModifyDto, Modify.Command>, ISectionService
    {
        public SectionService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public async Task<IEnumerable<SectionDto>> GetAsync()
        {
            return await base.GetAsync(orderBy: s => s.OrderBy(s => s.Name));
        }
    }
}
