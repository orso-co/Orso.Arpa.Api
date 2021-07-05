using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Educations;

namespace Orso.Arpa.Application.Services
{
    public class EducationService : BaseService<
        EducationDto,
        Education,
        EducationCreateDto,
        Create.Command,
        EducationModifyDto,
        EducationModifyBodyDto,
        Modify.Command
        >, IEducationService
    {
        public EducationService(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public override Task<IEnumerable<EducationDto>> GetAsync(
            Expression<Func<Education, bool>> predicate = null,
            Func<IQueryable<Education>, IOrderedQueryable<Education>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            return base.GetAsync(orderBy: orderBy ?? (e => e.OrderBy(ed => ed.SortOrder)));
        }
    }
}
