using System.Threading.Tasks;
using Orso.Arpa.Application.AudienceApplication.Model;

namespace Orso.Arpa.Application.AudienceApplication.Interfaces
{
    public interface IAudienceService
    {
        Task<AudienceSearchResultDto> SearchAsync(AudienceSearchDto searchDto);
    }
}
