using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.EducationApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IEducationService
    {
        Task<EducationDto> GetByIdAsync(Guid id);
        Task<EducationDto> CreateAsync(EducationCreateDto createEducationDto);
        Task ModifyAsync(EducationModifyDto EducationModifyDto);
        Task DeleteAsync(Guid id);
    }
}
