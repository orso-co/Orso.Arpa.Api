using System;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.GenericHandlers.Create;

namespace Orso.Arpa.Domain.Logic.ProjectParticipations
{
    public static class Create
    {
        public class Command : ICreateCommand<ProjectParticipation>
        {
            public Guid ProjectId { get; set; }
            public Guid MusicianProfileId { get; set; }
            public string CommentByStaffInner { get; set; }
            public Guid? ParticipationStatusInnerId { get; set; }
            public Guid? ParticipationStatusInternalId { get; set; }
            public Guid? InvitationStatusId { get; set; }
            public string CommentTeam { get; set; }
        }
    }
}
