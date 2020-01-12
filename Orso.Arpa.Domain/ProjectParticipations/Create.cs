using System;
using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.ProjectParticipations
{
    public static class Create
    {
        public class Command : IRequest<ProjectParticipation>
        {
            public Guid ProjectId { get; set; }
            public Guid MusicianProfileId { get; set; }
        }
    }
}
