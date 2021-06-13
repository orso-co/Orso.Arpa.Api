using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.TranslationApplication;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class TranslationController : BaseController
    {
        private readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SetTranslations([FromBody] TranslationDto translationDto, [FromQuery] string culture)
        {
            await _translationService.ModifyAsync(translationDto, culture);
            return NoContent();
        }

        [Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<TranslationDto> GetTranslations([FromQuery] string culture)
        {
            return await _translationService.GetAsync(culture);
        }
    }
}
