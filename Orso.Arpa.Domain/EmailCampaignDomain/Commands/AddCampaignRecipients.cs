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

public static class AddCampaignRecipients
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
                    new ValidationFailure(nameof(campaign.Status), "Recipients can only be added to draft campaigns.") { ErrorCode = "422" }
                });
            }

            // Get existing recipient PersonIds for this campaign
            var existingPersonIds = await _arpaContext.Set<EmailCampaignRecipient>()
                .Where(r => r.EmailCampaignId == request.CampaignId && !r.Deleted)
                .Select(r => r.PersonId)
                .ToListAsync(cancellationToken);

            // Get unsubscribed email addresses
            var unsubscribedPersonIds = await _arpaContext.Set<EmailUnsubscription>()
                .Where(u => !u.Deleted)
                .Select(u => u.PersonId)
                .ToListAsync(cancellationToken);

            var existingSet = new HashSet<Guid>(existingPersonIds);
            var unsubscribedSet = new HashSet<Guid>(unsubscribedPersonIds);
            int addedCount = 0;

            foreach (Guid personId in request.PersonIds.Distinct())
            {
                if (existingSet.Contains(personId) || unsubscribedSet.Contains(personId))
                {
                    continue;
                }

                var person = await _arpaContext.FindAsync<PersonDomain.Model.Person>(new object[] { personId }, cancellationToken);
                if (person == null)
                {
                    continue;
                }

                string email = person.GetPreferredEMailAddress();
                if (string.IsNullOrWhiteSpace(email))
                {
                    continue;
                }

                var recipient = new EmailCampaignRecipient(
                    Guid.NewGuid(),
                    request.CampaignId,
                    personId,
                    email,
                    person.DisplayName);

                _arpaContext.Add(recipient);
                addedCount++;
            }

            if (addedCount > 0)
            {
                campaign.SetTotalRecipients(existingPersonIds.Count + addedCount);
                await _arpaContext.SaveChangesAsync(cancellationToken);
            }

            return addedCount;
        }
    }
}
