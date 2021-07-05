using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public override Task<IEnumerable<CurriculumVitaeReferenceDto>> GetAsync(
            Expression<Func<CurriculumVitaeReference, bool>> predicate = null,
            Func<IQueryable<CurriculumVitaeReference>, IOrderedQueryable<CurriculumVitaeReference>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            return base.GetAsync(orderBy: orderBy ?? (e => e.OrderBy(ed => ed.SortOrder)));
        }
    }
}
