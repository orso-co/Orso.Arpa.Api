using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class SendProjectParticipationChangedByStaffInfo
    {
        public class Command : IRequest
        {
            public ProjectParticipation ProjectParticipation { get; init; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                _ = RuleFor(c => c.ProjectParticipation)
                    .NotNull();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IEmailSender _emailSender;
            private readonly ILogger<Handler> _logger;

            public Handler(IEmailSender emailSender, ILogger<Handler> logger)
            {
                _emailSender = emailSender;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Person musician = request.ProjectParticipation.MusicianProfile.Person;

                var template = new ProjectParticipationChangedByStaffTemplate
                {
                    CommentByStaff = request.ProjectParticipation.CommentByStaffInner ?? "- ohne -",
                    Comment = request.ProjectParticipation.CommentByPerformerInner ?? "- ohne -",
                    MusicianName = musician.DisplayName,
                    ParticipationStatusInner = request.ProjectParticipation.ParticipationStatusInner?.ToString() ?? "- ohne -",
                    ParticipationStatusInternal = request.ProjectParticipation.ParticipationStatusInternal?.ToString() ?? "- ohne -",

                    InvitationStatus = request.ProjectParticipation?.InvitationStatus?.ToString() ?? "- ohne -",
                    ProjectName = request.ProjectParticipation.Project.ToString()
                };

                var emailAddress = musician.GetPreferredEMailAddress();

                if (emailAddress != null)
                {
                    await _emailSender.SendTemplatedEmailAsync(template, emailAddress);
                }
                else
                {
                    _logger.LogError("Could not send the project participation changed email to {musician} because there is no email address assigned to this person.", musician.DisplayName);
                }

                return Unit.Value;
            }
        }
    }
}
