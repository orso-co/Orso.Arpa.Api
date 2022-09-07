using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.MyProjects
{
    public static class SendProjectParticipationChangedInfo
    {
        public class Command : IRequest
        {
            public ProjectParticipation ProjectParticipation { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.ProjectParticipation)
                    .NotNull();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ClubConfiguration _clubConfiguration;
            private readonly IEmailSender _emailSender;

            public Handler(ClubConfiguration clubConfiguration,
                           IEmailSender emailSender)
            {
                _clubConfiguration = clubConfiguration;
                _emailSender = emailSender;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var template = new ProjectParticipationChangedTemplate
                {
                    CommentByStaff = request.ProjectParticipation.CommentByStaffInner,
                    Comment = request.ProjectParticipation.CommentByPerformerInner,
                    MusicianName = request.ProjectParticipation.MusicianProfile.Person.DisplayName,
                    ParticipationStatus = request.ProjectParticipation.ParticipationStatusInner.SelectValue.Name,
                    ParticipationStatusInternal = request.ProjectParticipation.ParticipationStatusInternal.SelectValue.Name,
                    ProjectName = request.ProjectParticipation.Project.ToString()
                };

                await _emailSender.SendTemplatedEmailAsync(template, _clubConfiguration.InternalEmail);

                return Unit.Value;
            }
        }
    }
}
