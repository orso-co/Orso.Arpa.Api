using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreateAsync(PersonCreateDto createDto);

        Task<PersonDto> GetByIdAsync(Guid id);

        Task<IEnumerable<PersonDto>> GetAsync(
            Expression<Func<Person, bool>> predicate = null,
            Func<IQueryable<Person>, IOrderedQueryable<Person>> orderBy = null,
            int? skip = null,
            int? take = null);

        Task ModifyAsync(PersonModifyDto modifyDto);

        Task DeleteAsync(Guid id);
    }
}
