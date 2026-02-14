using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Queries;

public static class GetCampaignAnalytics
{
    public class Query : IRequest<Result>
    {
        public Guid CampaignId { get; set; }
    }

    public class Result
    {
        public int TotalRecipients { get; set; }
        public int SentCount { get; set; }
        public int FailedCount { get; set; }
        public int OpenedCount { get; set; }
        public int PendingCount { get; set; }
        public double OpenRate { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly IArpaContext _arpaContext;

        public Handler(IArpaContext arpaContext)
        {
            _arpaContext = arpaContext;
        }

        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var recipients = await _arpaContext.Set<EmailCampaignRecipient>()
                .Where(r => r.EmailCampaignId == request.CampaignId && !r.Deleted)
                .ToListAsync(cancellationToken);

            int total = recipients.Count;
            int sent = recipients.Count(r => r.Status == Enums.RecipientStatus.Sent);
            int failed = recipients.Count(r => r.Status == Enums.RecipientStatus.Failed || r.Status == Enums.RecipientStatus.Bounced);
            int opened = recipients.Count(r => r.OpenedAt != null);
            int pending = recipients.Count(r => r.Status == Enums.RecipientStatus.Pending);

            return new Result
            {
                TotalRecipients = total,
                SentCount = sent,
                FailedCount = failed,
                OpenedCount = opened,
                PendingCount = pending,
                OpenRate = sent > 0 ? Math.Round((double)opened / sent * 100, 1) : 0
            };
        }
    }
}
