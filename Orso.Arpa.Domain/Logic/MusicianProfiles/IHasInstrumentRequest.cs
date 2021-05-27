using System;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public interface IHasInstrumentRequest
    {
        public Guid InstrumentId { get; set; }
    }
}
