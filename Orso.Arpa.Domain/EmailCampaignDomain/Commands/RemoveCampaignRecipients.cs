using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class RemoveCampaignRecipients
{
    public class Command : IRequest<int>
    {
        public Guid CampaignId { get; set; }
        public IList<Guid> PersonIds { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.CampaignId)
                .EntityExists<Command, EmailCampaign>(arpaContext);

            _ = RuleFor(d => d.PersonIds)
                .NotEmpty()
                .WithMessage("At least one person must be specified.");
        }
    }

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            EmailCampaign campaign = await _arpaContext.FindAsync<EmailCampaign>(new object[] { request.CampaignId }, cancellationToken)
                ?? throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(request.CampaignId), "Campaign not found.") { ErrorCode = "404" }
                });

            if (campaign.Status != CampaignStatus.Draft)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(campaign.Status), "Recipients can only be removed from draft campaigns.") { ErrorCode = "422" }
                });
            }

            var personIdSet = new HashSet<Guid>(request.PersonIds.Distinct());

            var recipientsToRemove = await _arpaContext.Set<EmailCampaignRecipient>()
                .Where(r => r.EmailCampaignId == request.CampaignId && !r.Deleted && personIdSet.Contains(r.PersonId))
                .ToListAsync(cancellationToken);

            int removedCount = recipientsToRemove.Count;

            foreach (var recipient in recipientsToRemove)
            {
                _arpaContext.Remove(recipient);
            }

            if (removedCount > 0)
            {
                // Count existing non-deleted recipients minus the ones we're about to remove
                int existingCount = await _arpaContext.Set<EmailCampaignRecipient>()
                    .CountAsync(r => r.EmailCampaignId == request.CampaignId && !r.Deleted, cancellationToken);

                campaign.SetTotalRecipients(existingCount - removedCount);
                await _arpaContext.SaveChangesAsync(cancellationToken);
            }

            return removedCount;
        }
    }
}
