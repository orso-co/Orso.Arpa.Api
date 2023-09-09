using System.Threading.Tasks;
using Orso.Arpa.Application.TranslationApplication.Model;

namespace Orso.Arpa.Application.TranslationApplication.Interfaces
{
    public interface ITranslationService
    {
        Task<TranslationDto> GetAsync(string culture);

        Task ModifyAsync(TranslationDto modifyDto, string culture);

    }
}
