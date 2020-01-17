using System;
using MediatR;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Domain.MusicianProfiles
{
    public static class Create
    {
        public class Command : IRequest<MusicianProfile>
        {
            public bool IsProfessional { get; set; }
            public Guid PersonId { get; set; }
            public Guid SectionId { get; set; }
        }
    }
}
