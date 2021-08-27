using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.BankAccountApplication;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Roles;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/persons/{id}/bankaccounts")]
    public class PersonBankAccountsController : BaseController
    {
        private readonly IBankAccountService _bankAccountService;

        public PersonBankAccountsController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        /// <summary>
        /// Adds a new bank account to an existing person
        /// </summary>
        /// <param name="bankAccountCreateDto"></param>
        /// <returns>The created bank account</returns>
        /// <response code="200"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<BankAccountDto>> Post(BankAccountCreateDto bankAccountCreateDto)
        {
            return Ok(await _bankAccountService.CreateAsync(bankAccountCreateDto));
        }

        /// <summary>
        /// Updates an existing bank account
        /// </summary>
        /// <param name="bankAccountModifyDto"></param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpPut("{bankAccountId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Put(BankAccountModifyDto bankAccountModifyDto)
        {
            await _bankAccountService.ModifyAsync(bankAccountModifyDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing bank account
        /// </summary>
        /// <param name="id">the person id</param>
        /// <param name="bankAccountId">the bank account id</param>
        /// <response code="204"></response>
        /// <response code="404">If entity could not be found</response>
        /// <response code="422">If validation fails</response>
        [Authorize(Roles = RoleNames.Staff)]
        [HttpDelete("{bankAccountId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid bankAccountId)
        {
            await _bankAccountService.DeleteAsync(bankAccountId);
            return NoContent();
        }
    }
}
