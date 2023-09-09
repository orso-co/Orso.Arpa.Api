using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Application.TranslationApplication.Interfaces;
using Orso.Arpa.Application.TranslationApplication.Model;
using Orso.Arpa.Domain.LocalizationDomain.Commands;
using Orso.Arpa.Domain.LocalizationDomain.Model;
using Orso.Arpa.Domain.LocalizationDomain.Queries;

namespace Orso.Arpa.Application.TranslationApplication.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TranslationService(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<TranslationDto> GetAsync(string culture)
        {
            IEnumerable<Localization> localizations = await _mediator.Send(new ListLocalizations.Query { Culture = culture });
            return _mapper.Map<TranslationDto>(localizations);
        }

        public async Task ModifyAsync(TranslationDto modifyDto, string culture)
        {
            List<Localization> localizations = _mapper.Map<List<Localization>>(modifyDto);
            localizations.ForEach(l => l.LocalizationCulture = culture);
            await _mediator.Send(new ModifyLocalization.Query { Culture = culture, Localizations = localizations });
        }
    }
}
