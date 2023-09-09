using System;
using System.Threading.Tasks;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;

namespace Orso.Arpa.Application.DoublingInstrumentApplication.Interfaces
{
    public interface IDoublingInstrumentService
    {
        Task<DoublingInstrumentDto> CreateAsync(DoublingInstrumentCreateDto doublingInstrumentCreateDto);
        Task ModifyAsync(DoublingInstrumentModifyDto doublingInstrumentModifyDto);
        Task DeleteAsync(Guid id);
    }
}
