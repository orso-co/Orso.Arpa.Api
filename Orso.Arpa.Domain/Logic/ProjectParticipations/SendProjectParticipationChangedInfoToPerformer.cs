using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class SendProjectParticipationChangedInfoToPerformer
    {
        public class Command : IRequest
        {
            public ProjectParticipation ProjectParticipation { get; set; }
            public string Username { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ArpaUserManager userManager)
            {
                RuleFor(c => c.Username)
                    .MustAsync(async (username, cancellation) => await userManager.FindByNameAsync(username) != null)
                    .WithErrorCode("404")
                    .WithMessage("User could not be found.");

                RuleFor(c => c.ProjectParticipation)
                    .NotNull();
            }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ArpaUserManager _userManager;
            private readonly IEmailSender _emailSender;

            public Handler(ArpaUserManager userManager,
                           IEmailSender emailSender)
            {
                _userManager = userManager;
                _emailSender = emailSender;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                User user = await _userManager.FindByNameAsync(request.Username);

                var template = new ProjectParticipationChangedTemplate
                {
                    CommentByStaff = request.ProjectParticipation.CommentByStaffInner ?? "- ohne -",
                    Comment = request.ProjectParticipation.CommentByPerformerInner ?? "- ohne -",
                    MusicianName = request.ProjectParticipation.MusicianProfile.Person.DisplayName,
                    ParticipationStatus = request.ProjectParticipation.ParticipationStatusInner.SelectValue.Name,
                    ParticipationStatusInternal = request.ProjectParticipation.ParticipationStatusInternal.SelectValue.Name,
                    ProjectName = request.ProjectParticipation.Project.ToString()
                };

                await _emailSender.SendTemplatedEmailAsync(template, user.Email);

                return Unit.Value;
            }
        }
    }
}
