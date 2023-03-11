using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Mail.Interfaces;
using Orso.Arpa.Mail.Templates;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public class ProjectParticipationChangedNotification : INotification
    {
        public ProjectParticipation ProjectParticipation { get; set; }

        public bool ChangedByPerformer { get; set; }
    }

    public class SendProjectParticipationChangedByPerformerMail : INotificationHandler<ProjectParticipationChangedNotification>
    {
        private readonly ClubConfiguration _clubConfiguration;
        private readonly IEmailSender _emailSender;

        public SendProjectParticipationChangedByPerformerMail(ClubConfiguration clubConfiguration,
                       IEmailSender emailSender)
        {
            _clubConfiguration = clubConfiguration;
            _emailSender = emailSender;
        }

        public async Task Handle(ProjectParticipationChangedNotification notification, CancellationToken cancellationToken)
        {
            if (!notification.ChangedByPerformer)
            {
                return;
            }

            var template = new ProjectParticipationChangedByPerformerTemplate
            {
                CommentByStaff = notification.ProjectParticipation.CommentByStaffInner ?? "- ohne -",
                Comment = notification.ProjectParticipation.CommentByPerformerInner ?? "- ohne -",
                MusicianName = notification.ProjectParticipation.MusicianProfile.Person.DisplayName,
                ParticipationStatus = notification.ProjectParticipation.ParticipationStatusInner?.ToString() ?? "- ohne -",
                ParticipationStatusInternal = notification.ProjectParticipation.ParticipationStatusInternal?.ToString() ?? "- ohne -",
                ProjectName = notification.ProjectParticipation.Project.ToString()
            };

            await _emailSender.SendTemplatedEmailAsync(template, _clubConfiguration.InternalEmail);
        }
    }

    public class SendProjectParticipationChangedByStaffMail : INotificationHandler<ProjectParticipationChangedNotification>
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<SendProjectParticipationChangedByStaffMail> _logger;
        private readonly ClubConfiguration _clubConfiguration;

        public SendProjectParticipationChangedByStaffMail(
            IEmailSender emailSender,
            ILogger<SendProjectParticipationChangedByStaffMail> logger,
            ClubConfiguration clubConfiguration)
        {
            _emailSender = emailSender;
            _logger = logger;
            _clubConfiguration = clubConfiguration;
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
                CommentByStaff = notification.ProjectParticipation.CommentByStaffInner ?? "- ohne -",
                Comment = notification.ProjectParticipation.CommentByPerformerInner ?? "- ohne -",
                MusicianName = musician.DisplayName,
                ParticipationStatusInner = notification.ProjectParticipation.ParticipationStatusInner?.ToString() ?? "- ohne -",
                ParticipationStatusInternal = notification.ProjectParticipation.ParticipationStatusInternal?.ToString() ?? "- ohne -",

                InvitationStatus = notification.ProjectParticipation?.InvitationStatus?.ToString() ?? "- ohne -",
                ProjectName = notification.ProjectParticipation.Project.ToString()
            };

            var emailAddress = musician.GetPreferredEMailAddress();

            if (emailAddress != null)
            {
                await _emailSender.SendTemplatedEmailAsync(template, new string[2] { emailAddress, _clubConfiguration.InternalEmail });
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

            await UpdateParentProjectAsync(notification.ProjectParticipation, !notification.ChangedByPerformer, cancellationToken);
        }

        private async Task UpdateParentProjectAsync(ProjectParticipation projectParticipation, bool updateInternalStatus, CancellationToken cancellationToken)
        {
            Project parentProject = projectParticipation.Project.Parent;

            Guid musicianProfileId = projectParticipation.MusicianProfileId;

            ProjectParticipation participationOfParentProject = parentProject.ProjectParticipations
                .FirstOrDefault(pp => musicianProfileId.Equals(pp.MusicianProfileId));

            IEnumerable<ProjectParticipation> participationStatusOfChildrenProjects = parentProject.Children
                .Select(child => child.ProjectParticipations
                    .FirstOrDefault(pp => musicianProfileId.Equals(pp.MusicianProfileId)));
            IEnumerable<ProjectParticipationStatusInner?> participationStatusInnerOfChildrenProjects = participationStatusOfChildrenProjects
                .Select(pp => pp?.ParticipationStatusInner);

            ProjectParticipationStatusInner expectedParticipationStatusInner = ProjectParticipationStatusInheritanceEvaluator
                .EvaluateNewParticpationStatusInner(participationStatusInnerOfChildrenProjects);
            bool shouldSetParticipationStatusInner = participationOfParentProject is null
                || !expectedParticipationStatusInner.Equals(participationOfParentProject.ParticipationStatusInner);

            ProjectParticipationStatusInternal expectedParticipationStatusInternal = 0;
            bool shouldSetParticipationStatusInternal = false;

            ProjectInvitationStatus expectedInvitationStatus = 0;
            bool shouldSetInvitationStatus = false;

            if (updateInternalStatus)
            {
                IEnumerable<ProjectParticipationStatusInternal?> participationStatusInternalOfChildrenProjects = participationStatusOfChildrenProjects
                    .Select(pp => pp?.ParticipationStatusInternal);
                expectedParticipationStatusInternal = ProjectParticipationStatusInheritanceEvaluator
                    .EvaluateNewParticipationStatusInternal(participationStatusInternalOfChildrenProjects);
                shouldSetParticipationStatusInternal = participationOfParentProject is null
                || !expectedParticipationStatusInternal.Equals(participationOfParentProject.ParticipationStatusInternal);

                IEnumerable<ProjectInvitationStatus?> invitationStatusOfChildrenProjects = participationStatusOfChildrenProjects
                    .Select(pp => pp?.InvitationStatus);
                expectedInvitationStatus = ProjectParticipationStatusInheritanceEvaluator
                    .EvaluateNewInvitationStatus(invitationStatusOfChildrenProjects);
                shouldSetInvitationStatus = participationOfParentProject is null
                    || !expectedInvitationStatus.Equals(participationOfParentProject.InvitationStatus);
            }

            if (shouldSetParticipationStatusInner
                || shouldSetParticipationStatusInternal
                || shouldSetInvitationStatus)
            {
                await CreateOrUpdateProjectParticipationAsync(
                       participationOfParentProject,
                       shouldSetParticipationStatusInner ? expectedParticipationStatusInner : null,
                       musicianProfileId,
                       parentProject.Id,
                       shouldSetParticipationStatusInternal ? expectedParticipationStatusInternal : null,
                       shouldSetInvitationStatus ? expectedInvitationStatus : null,
                       cancellationToken);

                if (parentProject.ParentId != null)
                {
                    await UpdateParentProjectAsync(participationOfParentProject, updateInternalStatus, cancellationToken);
                }
            }
        }

        private async Task CreateOrUpdateProjectParticipationAsync(
            ProjectParticipation projectParticipation,
            ProjectParticipationStatusInner? projectParticipationStatusInner,
            Guid musicianProfileId,
            Guid projectId,
            ProjectParticipationStatusInternal? projectParticipationStatusInternal,
            ProjectInvitationStatus? projectInvitationStatus,
            CancellationToken cancellationToken)
        {
            if (projectParticipation is null)
            {
                var newParticipation = new ProjectParticipation(
                    new SetProjectParticipation.Command
                    {
                        MusicianProfileId = musicianProfileId,
                        ProjectId = projectId,
                        ParticipationStatusInner = projectParticipationStatusInner,
                        ParticipationStatusInternal = projectParticipationStatusInternal,
                        InvitationStatus = projectInvitationStatus,
                    });

                _ = _arpaContext.Add(newParticipation);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _logger.LogInformation("Participation of parent project created due to inheritance. " +
                        "parentProjectId={ProjectId} " +
                        "musicianProfileId={MusicianProfileId}" +
                        "participationStatusInner={ParticipationStatusInner} " +
                        "participationStatusInternal={ParticipationStatusInternal} " +
                        "invitationStatus={ProjectInvitationStatus}",
                        projectId, musicianProfileId, projectParticipationStatusInner, projectParticipationStatusInternal, projectInvitationStatus);
                    return;
                }

                throw new Exception($"Problem creating {nameof(ProjectParticipation)}");
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

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _logger.LogInformation("Participation of parent project updated due to inheritance (null value means no update). " +
                        "parentProjectId={ProjectId} " +
                        "musicianProfileId={MusicianProfileId}" +
                        "participationStatusInner={ParticipationStatusInner} " +
                        "participationStatusInternal={ParticipationStatusInternal} " +
                        "invitationStatus={ProjectInvitationStatus}",
                        projectId, musicianProfileId, projectParticipationStatusInner, projectParticipationStatusInternal, projectInvitationStatus);
                    return;
                }

                throw new Exception($"Problem updating {nameof(ProjectParticipation)}");
            }
        }
    }
}
