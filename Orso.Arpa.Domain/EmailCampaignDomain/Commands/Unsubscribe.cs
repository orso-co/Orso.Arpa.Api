using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class Unsubscribe
{
    public class Command : IRequest
    {
        public Guid TrackingToken { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            EmailCampaignRecipient recipient = await _arpaContext.Set<EmailCampaignRecipient>()
                .FirstOrDefaultAsync(r => r.TrackingToken == request.TrackingToken && !r.Deleted, cancellationToken)
                ?? throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(request.TrackingToken), "Invalid tracking token.") { ErrorCode = "404" }
                });

            // Check if already unsubscribed
            bool alreadyUnsubscribed = await _arpaContext.Set<EmailUnsubscription>()
                .AnyAsync(u => u.PersonId == recipient.PersonId && !u.Deleted, cancellationToken);

            if (!alreadyUnsubscribed)
            {
                var unsubscription = new EmailUnsubscription(
                    Guid.NewGuid(),
                    recipient.PersonId,
                    recipient.EmailAddress,
                    request.Reason);

                _arpaContext.Add(unsubscription);
                await _arpaContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
