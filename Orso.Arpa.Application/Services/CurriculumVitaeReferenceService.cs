using AutoMapper;
using MediatR;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.CurriculumVitaeReferences;

namespace Orso.Arpa.Application.Services
{
    public class CurriculumVitaeReferenceService : BaseService<
        CurriculumVitaeReferenceDto,
        CurriculumVitaeReference,
        CurriculumVitaeReferenceCreateDto,
        Create.Command,
        CurriculumVitaeReferenceModifyDto,
        CurriculumVitaeReferenceModifyBodyDto,
        Modify.Command
        >, ICurriculumVitaeReferenceService
    {
        public CurriculumVitaeReferenceService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
