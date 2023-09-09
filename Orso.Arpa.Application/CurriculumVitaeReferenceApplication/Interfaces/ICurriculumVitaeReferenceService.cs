using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Model;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Interfaces
{
    public interface ICurriculumVitaeReferenceService
    {
        Task<CurriculumVitaeReferenceDto> GetByIdAsync(Guid id);
        Task<CurriculumVitaeReferenceDto> CreateAsync(CurriculumVitaeReferenceCreateDto createCurriculumVitaeReferenceDto);
        Task ModifyAsync(CurriculumVitaeReferenceModifyDto curriculumVitaeReferenceModifyDto);
        Task DeleteAsync(Guid id);
    }
}
