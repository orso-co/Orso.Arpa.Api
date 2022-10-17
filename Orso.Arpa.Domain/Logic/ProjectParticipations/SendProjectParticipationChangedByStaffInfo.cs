using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class SendProjectParticipationChangedByStaffInfo
    {
        public class Command : IRequest
        {
            public ProjectParticipation ProjectParticipation { get; set; }
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
            private readonly IUserAccessor _userAccessor;

            public Handler(IEmailSender emailSender,
                            IUserAccessor userAccessor)
            {
                _emailSender = emailSender;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var template = new ProjectParticipationChangedByStaffTemplate
                {
                    CommentByStaff = request.ProjectParticipation.CommentByStaffInner ?? "- ohne -",
                    Comment = request.ProjectParticipation.CommentByPerformerInner ?? "- ohne -",
                    MusicianName = _userAccessor.DisplayName,
                    ParticipationStatus = request.ProjectParticipation.ParticipationStatusInner.SelectValue.Name,
                    ParticipationStatusInternal = request.ProjectParticipation.ParticipationStatusInternal.SelectValue.Name,
                    ProjectName = request.ProjectParticipation.Project.ToString()
                };

                User user = await _userAccessor.GetCurrentUserAsync(cancellationToken);
                await _emailSender.SendTemplatedEmailAsync(template, user.Email);

                return Unit.Value;
            }
        }
    }
}
