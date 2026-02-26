using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Api.Controllers
{
    [Route("api/me/memberships")]
    public class MyMembershipsController : BaseController
    {
        private readonly IArpaContext _arpaContext;
        private readonly ITokenAccessor _tokenAccessor;

        public MyMembershipsController(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
        {
            _arpaContext = arpaContext;
            _tokenAccessor = tokenAccessor;
        }

        /// <summary>
        /// Gets the current user's memberships (performer view, no staff-internal fields)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MyMembershipDto>>> GetMyMemberships(CancellationToken cancellationToken)
        {
            var personId = _tokenAccessor.PersonId;
            if (personId == Guid.Empty)
            {
                return Ok(new List<MyMembershipDto>());
            }

            var memberships = await _arpaContext.Set<PersonMembership>()
                .Where(m => m.PersonId == personId && !m.Deleted)
                .Include(m => m.SupportLevel).ThenInclude(s => s.SelectValue)
                .Include(m => m.MembershipStatus).ThenInclude(s => s.SelectValue)
                .Include(m => m.PaymentMethod).ThenInclude(s => s.SelectValue)
                .Include(m => m.PaymentFrequency).ThenInclude(s => s.SelectValue)
                .Include(m => m.Club).ThenInclude(s => s.SelectValue)
                .Include(m => m.MembershipHistories.Where(h => !h.Deleted))
                .OrderByDescending(m => m.EntryDate)
                .ToListAsync(cancellationToken);

            // Also load bank accounts for IBAN display
            var bankAccounts = await _arpaContext.Set<BankAccount>()
                .Where(ba => ba.PersonId == personId && !ba.Deleted)
                .ToListAsync(cancellationToken);

            var result = memberships.Select(m => new MyMembershipDto
            {
                Id = m.Id,
                EntryDate = m.EntryDate,
                ExitDate = m.ExitDate,
                AnnualFee = m.AnnualFee,
                SupportLevel = m.SupportLevel?.SelectValue?.Name,
                MembershipStatus = m.MembershipStatus?.SelectValue?.Name,
                PaymentMethod = m.PaymentMethod?.SelectValue?.Name,
                PaymentFrequency = m.PaymentFrequency?.SelectValue?.Name,
                Club = m.Club?.SelectValue?.Name,
                MandateReference = m.MandateReference,
                MandateDate = m.MandateDate,
                Iban = MaskIban(bankAccounts.FirstOrDefault()?.Iban),
                History = m.MembershipHistories
                    .OrderByDescending(h => h.Year)
                    .Select(h => new MyMembershipHistoryDto
                    {
                        Year = h.Year,
                        Amount = h.Amount,
                        IsReduced = h.IsReduced,
                    }).ToList(),
            }).ToList();

            return Ok(result);
        }

        private static string MaskIban(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban) || iban.Length < 6)
            {
                return null;
            }
            // Show first 2 chars (country) + last 4 chars, mask the rest
            return iban[..2] + new string('*', iban.Length - 6) + iban[^4..];
        }
    }

    public class MyMembershipDto
    {
        public Guid Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public decimal AnnualFee { get; set; }
        public string SupportLevel { get; set; }
        public string MembershipStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentFrequency { get; set; }
        public string Club { get; set; }
        public string MandateReference { get; set; }
        public DateTime? MandateDate { get; set; }
        public string Iban { get; set; }
        public List<MyMembershipHistoryDto> History { get; set; } = new();
    }

    public class MyMembershipHistoryDto
    {
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public bool IsReduced { get; set; }
    }
}
