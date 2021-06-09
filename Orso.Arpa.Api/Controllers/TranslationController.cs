using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.TranslationApplication;

namespace Orso.Arpa.Api.Controllers
{
    public class TranslationController : BaseController
    {
        public readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        // TODO: disallow anonymous access.
        [AllowAnonymous]
        //[Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),
            StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SetTranslations([FromBody] TranslationDto translationDto, [FromQuery] string culture)
        {

            await _translationService.ModifyAsync(translationDto, culture);
            return NoContent();
        }


        // TODO: disallow anonymous access.
        [AllowAnonymous]
        //[Authorize(Policy = AuthorizationPolicies.AtLeastStaffPolicy)]
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
