using System;
using Microsoft.EntityFrameworkCore;

namespace Orso.Arpa.Domain.Views
{
    [Keyless]
    public class Musician
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public Guid PersonId { get; set; }
        public Guid MusicianProfileId { get; set; }
        public string InstrumentName { get; set; }
        public Guid InstrumentId { get; set; }
    }
}
