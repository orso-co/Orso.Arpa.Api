using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class DuplicateEmailCampaign
{
    public class Command : IRequest<Guid>
    {
        public Guid CampaignId { get; set; }
        public bool IncludeRecipients { get; set; } = true;
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.CampaignId)
                .EntityExists<Command, EmailCampaign>(arpaContext);
        }
    }

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            // Load source campaign
            EmailCampaign sourceCampaign = await _arpaContext.Set<EmailCampaign>()
                .FirstOrDefaultAsync(c => c.Id == request.CampaignId && !c.Deleted, cancellationToken);

            // Create new campaign via the constructor (same pattern as GenericHandlers.Create)
            var createCommand = new CreateEmailCampaign.Command
            {
                Name = $"Kopie: {sourceCampaign.Name}",
                Subject = sourceCampaign.Subject,
                EmailTemplateId = sourceCampaign.EmailTemplateId,
            };

            var newCampaign = new EmailCampaign(Guid.NewGuid(), createCommand);
            _arpaContext.Add(newCampaign);
            await _arpaContext.SaveChangesAsync(cancellationToken);

            // Copy PersonalizedHtml if present
            if (!string.IsNullOrEmpty(sourceCampaign.PersonalizedHtml))
            {
                var modifyCommand = new ModifyEmailCampaign.Command
                {
                    Id = newCampaign.Id,
                    Name = newCampaign.Name,
                    Subject = newCampaign.Subject,
                    EmailTemplateId = newCampaign.EmailTemplateId,
                    PersonalizedHtml = sourceCampaign.PersonalizedHtml,
                };

                // Reload to get tracked entity
                _arpaContext.ClearChangeTracker();
                var trackedCampaign = await _arpaContext.FindAsync<EmailCampaign>(new object[] { newCampaign.Id }, cancellationToken);
                trackedCampaign.Update(modifyCommand);
                await _arpaContext.SaveChangesAsync(cancellationToken);
            }

            // Copy recipients if requested
            if (request.IncludeRecipients)
            {
                List<EmailCampaignRecipient> sourceRecipients = await _arpaContext.Set<EmailCampaignRecipient>()
                    .Where(r => r.EmailCampaignId == request.CampaignId && !r.Deleted)
                    .ToListAsync(cancellationToken);

                // Get unsubscribed person IDs
                var unsubscribedPersonIds = await _arpaContext.Set<EmailUnsubscription>()
                    .Where(u => !u.Deleted)
                    .Select(u => u.PersonId)
                    .ToListAsync(cancellationToken);
                var unsubscribedSet = new HashSet<Guid>(unsubscribedPersonIds);

                int addedCount = 0;
                foreach (EmailCampaignRecipient sourceRecipient in sourceRecipients)
                {
                    if (unsubscribedSet.Contains(sourceRecipient.PersonId))
                    {
                        continue;
                    }

                    // Get fresh email from person
                    var person = await _arpaContext.FindAsync<PersonDomain.Model.Person>(
                        new object[] { sourceRecipient.PersonId }, cancellationToken);
                    if (person == null)
                    {
                        continue;
                    }

                    string email = person.GetPreferredEMailAddress();
                    if (string.IsNullOrWhiteSpace(email))
                    {
                        continue;
                    }

                    var newRecipient = new EmailCampaignRecipient(
                        Guid.NewGuid(),
                        newCampaign.Id,
                        sourceRecipient.PersonId,
                        email,
                        person.DisplayName);

                    _arpaContext.Add(newRecipient);
                    addedCount++;
                }

                if (addedCount > 0)
                {
                    // Reload campaign to update TotalRecipients
                    _arpaContext.ClearChangeTracker();
                    var campaignToUpdate = await _arpaContext.FindAsync<EmailCampaign>(
                        new object[] { newCampaign.Id }, cancellationToken);
                    campaignToUpdate.SetTotalRecipients(addedCount);
                    await _arpaContext.SaveChangesAsync(cancellationToken);
                }
            }

            return newCampaign.Id;
        }
    }
}
