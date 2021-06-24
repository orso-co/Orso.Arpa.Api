using System.Threading.Tasks;
using Orso.Arpa.Application.MusicianProfileApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface IDoublingInstrumentService
    {
        Task<DoublingInstrumentDto> CreateAsync(DoublingInstrumentCreateDto doublingInstrumentCreateDto);
    }
}
