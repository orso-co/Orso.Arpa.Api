using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/me/profiles/musician/{id}/documents")]
    public class MyMusicianProfileDocumentsController : BaseController
    {
        private readonly IMeService _meService;

        public MyMusicianProfileDocumentsController(IMeService meService)
        {
            _meService = meService;
        }

        /// <summary>
        /// Adds a document to an existing musician profile
        /// </summary>
        /// <param name="addDocumentDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpPost("{documentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> AddDocument([FromRoute] MyMusicianProfileAddDocumentDto addDocumentDto)
        {
            await _meService.AddDocumentToMusicienProfileAsync(addDocumentDto);
            return NoContent();
        }

        /// <summary>
        /// Removes document from existing musician profile
        /// </summary>
        /// <param name="removeDocumentDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Performer)]
        [HttpDelete("{documentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RemoveDocument([FromRoute] MyMusicianProfileRemoveDocumentDto removeDocumentDto)
        {
            await _meService.RemoveDocumentFromMusicianProfileAsync(removeDocumentDto);
            return NoContent();
        }
    }
}
