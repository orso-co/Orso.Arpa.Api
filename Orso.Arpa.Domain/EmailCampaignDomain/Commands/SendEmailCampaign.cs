using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Orso.Arpa.Domain.EmailCampaignDomain.Enums;
using Orso.Arpa.Domain.EmailCampaignDomain.Model;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.EmailCampaignDomain.Commands;

public static class SendEmailCampaign
{
    public class Command : IRequest
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator(IArpaContext arpaContext)
        {
            _ = RuleFor(d => d.Id)
                .EntityExists<Command, EmailCampaign>(arpaContext);
        }
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
            EmailCampaign campaign = await _arpaContext.FindAsync<EmailCampaign>(new object[] { request.Id }, cancellationToken)
                ?? throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(request.Id), "Campaign not found.") { ErrorCode = "404" }
                });

            if (campaign.Status != CampaignStatus.Draft && campaign.Status != CampaignStatus.Scheduled)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(campaign.Status), "Only draft or scheduled campaigns can be sent.") { ErrorCode = "422" }
                });
            }

            campaign.StartSending();
            await _arpaContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
