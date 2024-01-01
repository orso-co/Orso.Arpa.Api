using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Domain.ProjectDomain.Model;
using Orso.Arpa.Domain.ProjectDomain.Util;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;
using Orso.Arpa.Misc.Logging;

namespace Orso.Arpa.Domain.ProjectDomain.Notifications
{
    public class ProjectParticipationChangedNotification : INotification
    {
        public ProjectParticipation ProjectParticipation { get; set; }

        public bool ChangedByPerformer { get; set; }
    }

    public class SendProjectParticipationChangedInfoToKbb : INotificationHandler<ProjectParticipationChangedNotification>
    {
        private readonly ILogger<SendProjectParticipationChangedInfoToKbb> _logger;

        public SendProjectParticipationChangedInfoToKbb(ILogger<SendProjectParticipationChangedInfoToKbb> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProjectParticipationChangedNotification notification, CancellationToken cancellationToken)
        {
            KbbInfoLogger.LogInfoForKbb(
                _logger,
                "project participation changed",
                new Dictionary<string, object>
                {
                    { "Project", notification.ProjectParticipation.Project },
                    { "Musician Profile", notification.ProjectParticipation.MusicianProfile },
                    { "Participation Status Inner", notification.ProjectParticipation.ParticipationStatusInner },
                    { "Comment by Performer", notification.ProjectParticipation.CommentByPerformerInner },
                    { "Participation Status Internal", notification.ProjectParticipation.ParticipationStatusInternal },
                    { "Comment by Staff", notification.ProjectParticipation.CommentByStaffInner },
                    { "Comment Team", notification.ProjectParticipation.CommentTeam },
                    { "Invitation Status", notification.ProjectParticipation.InvitationStatus },
                    { "Modified by", notification.ProjectParticipation.ModifiedBy }
                });
            return Task.CompletedTask;
        }
    }

    public class SendProjectParticipationChangedMailToPerformer : INotificationHandler<ProjectParticipationChangedNotification>
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly ClubConfiguration _clubConfiguration;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<SendProjectParticipationChangedMailToPerformer> _logger;

        public SendProjectParticipationChangedMailToPerformer(
            JwtConfiguration jwtConfiguration,
            ClubConfiguration _clubConfiguration,
            IEmailSender emailSender,
            ILogger<SendProjectParticipationChangedMailToPerformer> logger)
        {
            _jwtConfiguration = jwtConfiguration;
            this._clubConfiguration = _clubConfiguration;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task Handle(ProjectParticipationChangedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.ChangedByPerformer)
            {
                return;
            }

            Person musician = notification.ProjectParticipation.MusicianProfile.Person;

            var template = new ProjectParticipationChangedByStaffTemplate
            {
                ArpaLogo = _jwtConfiguration.ArpaLogo,
                CommentByStaff = notification.ProjectParticipation.CommentByStaffInner ?? "- ohne -",
                Comment = notification.ProjectParticipation.CommentByPerformerInner ?? "- ohne -",
                DisplayName = musician.DisplayName,
                ParticipationStatusInner = notification.ProjectParticipation.ParticipationStatusInner?.ToString() ?? "- ohne -",
                ParticipationStatusInternal = notification.ProjectParticipation.ParticipationStatusInternal?.ToString() ?? "- ohne -",

                InvitationStatus = notification.ProjectParticipation?.InvitationStatus?.ToString() ?? "- ohne -",
                ProjectName = notification.ProjectParticipation.Project.ToString(),

                ClubAddress = _clubConfiguration.Address,
                ClubMail = _clubConfiguration.ContactEmail,
                ClubName = _clubConfiguration.Name,
                ClubPhoneNumber = _clubConfiguration.Phone
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
        }
    }

    public class CleanUpProjectParticipationStatusOfParentProjects : INotificationHandler<ProjectParticipationChangedNotification>
    {
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<CleanUpProjectParticipationStatusOfParentProjects> _logger;

        public CleanUpProjectParticipationStatusOfParentProjects(IArpaContext arpaContext, ILogger<CleanUpProjectParticipationStatusOfParentProjects> logger)
        {
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task Handle(ProjectParticipationChangedNotification notification, CancellationToken cancellationToken)
        {
            if (notification.ProjectParticipation.Project.ParentId is null)
            {
                return;
            }

            await UpdateParentProjectAsync(notification.ProjectParticipation, cancellationToken);
        }

        private async Task UpdateParentProjectAsync(ProjectParticipation projectParticipation, CancellationToken cancellationToken)
        {
            Project parentProject = projectParticipation.Project.Parent;

            Guid musicianProfileId = projectParticipation.MusicianProfileId;

            ProjectParticipation participationOfParentProject = parentProject.ProjectParticipations
                .FirstOrDefault(pp => musicianProfileId.Equals(pp.MusicianProfileId));

            IEnumerable<ProjectParticipation> participationStatusOfChildrenProjects = parentProject.Children
                .Select(child => child.ProjectParticipations
                    .FirstOrDefault(pp => musicianProfileId.Equals(pp.MusicianProfileId)));

            EvaluatedParticipationStatusInner(participationOfParentProject, participationStatusOfChildrenProjects, out ProjectParticipationStatusInner expectedParticipationStatusInner, out bool shouldSetParticipationStatusInner);

            EvaluateParticipationStatusInternal(participationOfParentProject, participationStatusOfChildrenProjects, out ProjectParticipationStatusInternal expectedParticipationStatusInternal, out bool shouldSetParticipationStatusInternal);

            EvaluateInvitationStatus(participationOfParentProject, participationStatusOfChildrenProjects, out ProjectInvitationStatus expectedInvitationStatus, out bool shouldSetInvitationStatus);

            if (shouldSetParticipationStatusInner
                || shouldSetParticipationStatusInternal
                || shouldSetInvitationStatus)
            {
                await CreateOrUpdateProjectParticipationAsync(
                       participationOfParentProject,
                       shouldSetParticipationStatusInner ? expectedParticipationStatusInner : null,
                       projectParticipation.MusicianProfile,
                       parentProject,
                       shouldSetParticipationStatusInternal ? expectedParticipationStatusInternal : null,
                       shouldSetInvitationStatus ? expectedInvitationStatus : null,
                       cancellationToken);

                if (parentProject.ParentId != null)
                {
                    await UpdateParentProjectAsync(participationOfParentProject, cancellationToken);
                }
            }
        }

        private static void EvaluateInvitationStatus(
            ProjectParticipation participationOfParentProject,
            IEnumerable<ProjectParticipation> participationStatusOfChildrenProjects,
            out ProjectInvitationStatus expectedInvitationStatus,
            out bool shouldSetInvitationStatus)
        {
            IEnumerable<ProjectInvitationStatus?> invitationStatusOfChildrenProjects = participationStatusOfChildrenProjects
                            .Select(pp => pp?.InvitationStatus);
            expectedInvitationStatus = ProjectParticipationStatusInheritanceEvaluator
                    .EvaluateNewInvitationStatus(invitationStatusOfChildrenProjects);
            shouldSetInvitationStatus = participationOfParentProject is null
                 || !expectedInvitationStatus.Equals(participationOfParentProject.InvitationStatus);
        }

        private static void EvaluateParticipationStatusInternal(
            ProjectParticipation participationOfParentProject,
            IEnumerable<ProjectParticipation> participationStatusOfChildrenProjects,
            out ProjectParticipationStatusInternal expectedParticipationStatusInternal,
            out bool shouldSetParticipationStatusInternal)
        {
            IEnumerable<ProjectParticipationStatusInternal?> participationStatusInternalOfChildrenProjects = participationStatusOfChildrenProjects
                            .Select(pp => pp?.ParticipationStatusInternal);
            expectedParticipationStatusInternal = ProjectParticipationStatusInheritanceEvaluator
                    .EvaluateNewParticipationStatusInternal(participationStatusInternalOfChildrenProjects);
            shouldSetParticipationStatusInternal = participationOfParentProject is null
            || !expectedParticipationStatusInternal.Equals(participationOfParentProject.ParticipationStatusInternal);
        }

        private static void EvaluatedParticipationStatusInner(
            ProjectParticipation participationOfParentProject,
            IEnumerable<ProjectParticipation> participationStatusOfChildrenProjects,
            out ProjectParticipationStatusInner expectedParticipationStatusInner,
            out bool shouldSetParticipationStatusInner)
        {
            IEnumerable<ProjectParticipationStatusInner?> participationStatusInnerOfChildrenProjects = participationStatusOfChildrenProjects
                .Select(pp => pp?.ParticipationStatusInner);
            expectedParticipationStatusInner = ProjectParticipationStatusInheritanceEvaluator
                .EvaluateNewParticpationStatusInner(participationStatusInnerOfChildrenProjects);
            shouldSetParticipationStatusInner = participationOfParentProject is null
                || !expectedParticipationStatusInner.Equals(participationOfParentProject.ParticipationStatusInner);
        }

        private async Task CreateOrUpdateProjectParticipationAsync(
            ProjectParticipation projectParticipation,
            ProjectParticipationStatusInner? projectParticipationStatusInner,
            MusicianProfile musicianProfile,
            Project parentProject,
            ProjectParticipationStatusInternal? projectParticipationStatusInternal,
            ProjectInvitationStatus? projectInvitationStatus,
            CancellationToken cancellationToken)
        {
            if (projectParticipation is null)
            {
                var newParticipation = new ProjectParticipation(
                    new SetProjectParticipation.Command
                    {
                        MusicianProfileId = musicianProfile.Id,
                        ProjectId = parentProject.Id,
                        ParticipationStatusInner = projectParticipationStatusInner,
                        ParticipationStatusInternal = projectParticipationStatusInternal,
                        InvitationStatus = projectInvitationStatus,
                    });

                _ = _arpaContext.Add(newParticipation);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) < 1)
                {
                    throw new AffectedRowCountMismatchException(nameof(projectParticipation));
                }

                KbbInfoLogger.LogInfoForKbb(
                    _logger,
                    "Participation of parent project created due to inheritance",
                    new Dictionary<string, object>
                    {
                            { "Parent Project", parentProject },
                            { "Musician Profile", musicianProfile },
                            { "Participation Status Inner", projectParticipationStatusInner },
                            { "Participation Status Internal", projectParticipationStatusInternal },
                            { "Invitation Status", projectInvitationStatus }
                    });
            }
            else
            {
                if (projectParticipationStatusInner.HasValue)
                {
                    projectParticipation.ParticipationStatusInner = projectParticipationStatusInner.Value;
                }
                if (projectParticipationStatusInternal.HasValue)
                {
                    projectParticipation.ParticipationStatusInternal = projectParticipationStatusInternal.Value;
                }
                if (projectInvitationStatus.HasValue)
                {
                    projectParticipation.InvitationStatus = projectInvitationStatus.Value;
                }
                _arpaContext.Entry(projectParticipation)?.CurrentValues?.SetValues(projectParticipation);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) < 1)
                {
                    throw new AffectedRowCountMismatchException(nameof(ProjectParticipation));
                }
                KbbInfoLogger.LogInfoForKbb(
                    _logger,
                    "Participation of parent project updated due to inheritance",
                    new Dictionary<string, object>
                    {
                            { "Parent Project", parentProject },
                            { "Musician Profile", musicianProfile },
                            { "Participation Status Inner", projectParticipationStatusInner },
                            { "Participation Status Internal", projectParticipationStatusInternal },
                            { "Invitation Status", projectInvitationStatus }
                    },
                    "NONE value means no update");
            }
        }
    }
}
