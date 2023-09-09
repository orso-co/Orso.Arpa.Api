using AutoMapper;
using MediatR;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Interfaces;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Model;
using Orso.Arpa.Application.General.Services;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Services
{
    public class CurriculumVitaeReferenceService : BaseService<
        CurriculumVitaeReferenceDto,
        CurriculumVitaeReference,
        CurriculumVitaeReferenceCreateDto,
        CreateCurriculumVitaeReference.Command,
        CurriculumVitaeReferenceModifyDto,
        CurriculumVitaeReferenceModifyBodyDto,
        ModifyCurriculumVitaeReference.Command
        >, ICurriculumVitaeReferenceService
    {
        public CurriculumVitaeReferenceService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
