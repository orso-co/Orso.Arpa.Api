using System.Threading.Tasks;
using Orso.Arpa.Application.TranslationApplication;

namespace Orso.Arpa.Application.Interfaces
{
    public interface ITranslationService
    {
        Task<TranslationDto> GetAsync(string culture);

        Task ModifyAsync(TranslationDto modifyDto, string culture);

    }
}
