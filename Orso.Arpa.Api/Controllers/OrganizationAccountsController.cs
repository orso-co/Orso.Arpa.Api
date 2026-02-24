using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Application.FinanceApplication.Interfaces;
using Orso.Arpa.Application.FinanceApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Api.Controllers
{
    public class OrganizationAccountsController : BaseController
    {
        private readonly IFinanceService _financeService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ITokenAccessor _tokenAccessor;
        private readonly ILogger<OrganizationAccountsController> _logger;

        public OrganizationAccountsController(
            IFinanceService financeService,
            IServiceScopeFactory scopeFactory,
            ITokenAccessor tokenAccessor,
            ILogger<OrganizationAccountsController> logger)
        {
            _financeService = financeService;
            _scopeFactory = scopeFactory;
            _tokenAccessor = tokenAccessor;
            _logger = logger;
        }

        private bool IsAdmin => _tokenAccessor.GetUserRoles().Contains(RoleNames.Admin);

        [Authorize(Roles = $"{RoleNames.Staff},{RoleNames.Admin}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrganizationAccountDto>>> GetAccounts()
        {
            return Ok(await _financeService.GetAccountsAsync(_tokenAccessor.UserId, IsAdmin));
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
            return Ok(await _financeService.GetBalanceSummaryAsync(_tokenAccessor.UserId, IsAdmin));
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

        // === Finance Club Access ===

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet("access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinanceClubAccessDto>>> GetFinanceAccesses()
        {
            return Ok(await _financeService.GetAllFinanceAccessesAsync());
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GrantAccess([FromBody] GrantFinanceAccessDto dto)
        {
            await _financeService.GrantFinanceAccessAsync(dto.UserId, dto.ClubId, _tokenAccessor.DisplayName);
            return Ok();
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("access/{userId}/{clubId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RevokeAccess(Guid userId, Guid clubId)
        {
            await _financeService.RevokeFinanceAccessAsync(userId, clubId);
            return Ok();
        }

        [Authorize(Roles = $"{RoleNames.Staff},{RoleNames.Admin}")]
        [HttpGet("my-access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MyFinanceAccessDto>> GetMyAccess()
        {
            var userId = _tokenAccessor.UserId;
            if (IsAdmin)
            {
                return Ok(new MyFinanceAccessDto { HasAccess = true, AccessibleClubIds = new List<Guid>() });
            }

            var clubIds = await _financeService.GetAccessibleClubIdsForUserAsync(userId);
            return Ok(new MyFinanceAccessDto
            {
                HasAccess = clubIds.Count > 0,
                AccessibleClubIds = clubIds
            });
        }
    }
}
