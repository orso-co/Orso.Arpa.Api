using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.FinanceApplication.Interfaces;
using Orso.Arpa.Application.FinanceApplication.Model;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class OrganizationAccountsController : BaseController
    {
        private readonly IFinanceService _financeService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrganizationAccountsController> _logger;

        public OrganizationAccountsController(
            IFinanceService financeService,
            IServiceScopeFactory scopeFactory,
            ILogger<OrganizationAccountsController> logger)
        {
            _financeService = financeService;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        [Authorize(Roles = $"{RoleNames.Staff},{RoleNames.Admin}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrganizationAccountDto>>> GetAccounts()
        {
            return Ok(await _financeService.GetAccountsAsync());
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<OrganizationAccountDto>> CreateAccount([FromBody] CreateOrganizationAccountDto dto)
        {
            var created = await _financeService.CreateAccountAsync(dto);
            return CreatedAtAction(nameof(GetAccounts), new { id = created.Id }, created);
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrganizationAccountDto>> UpdateAccount(Guid id, [FromBody] ModifyOrganizationAccountDto dto)
        {
            return Ok(await _financeService.UpdateAccountAsync(id, dto));
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            await _financeService.DeleteAccountAsync(id);
            return NoContent();
        }

        [Authorize(Roles = $"{RoleNames.Staff},{RoleNames.Admin}")]
        [HttpGet("balances")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BalanceSummaryDto>> GetBalances()
        {
            return Ok(await _financeService.GetBalanceSummaryAsync());
        }

        [Authorize(Roles = $"{RoleNames.Staff},{RoleNames.Admin}")]
        [HttpGet("{id}/balance-history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BalanceSnapshotDto>>> GetBalanceHistory(Guid id, [FromQuery] int days = 30)
        {
            return Ok(await _financeService.GetBalanceHistoryAsync(id, days));
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("{id}/sync")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult TriggerSync(Guid id)
        {
            // Fire-and-forget: FinTS sync can take minutes (TAN wait)
            _ = Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IFinanceService>();
                try
                {
                    await service.TriggerSyncAsync(id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Background sync failed for account {AccountId}", id);
                }
            });
            return Accepted();
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet("tan-requests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PendingTanRequestDto>>> GetTanRequests()
        {
            return Ok(await _financeService.GetPendingTanRequestsAsync());
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("tan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SubmitTan([FromBody] SubmitTanDto dto)
        {
            await _financeService.SubmitTanAsync(dto);
            return Ok();
        }
    }
}
