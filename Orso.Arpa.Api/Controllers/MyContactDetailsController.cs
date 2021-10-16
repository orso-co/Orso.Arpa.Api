using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Application.MyContactDetailApplication;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/me/contactdetails")]
    public class MyContactDetailsController : BaseController
    {
        private readonly IMyContactDetailService _myContactDetailService;

        public MyContactDetailsController(IMyContactDetailService myContactDetailService)
        {
            _myContactDetailService = myContactDetailService;
        }

        /// <summary>
        /// Adds new contact details
        /// </summary>
        /// <param name="myContactDetailCreateDto"></param>
        /// <returns>The created contact detail</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ContactDetailDto>> Post([FromBody] MyContactDetailCreateDto myContactDetailCreateDto)
        {
            return Ok(await _myContactDetailService.CreateAsync(myContactDetailCreateDto));
        }

        /// <summary>
        /// Updates an existing contact detail
        /// </summary>
        /// <param name="myContactDetailModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(MyContactDetailModifyDto myContactDetailModifyDto)
        {
            await _myContactDetailService.ModifyAsync(myContactDetailModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing contact detail
        /// </summary>
        /// <param name="myContactDetailDeleteDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(MyContactDetailDeleteDto myContactDetailDeleteDto)
        {
            await _myContactDetailService.DeleteAsync(myContactDetailDeleteDto);
            return NoContent();
        }
    }
}
