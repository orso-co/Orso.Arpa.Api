using System;

namespace Orso.Arpa.Domain.Logic.MusicianProfileDomain.Interfaces
{
    public interface IHasInstrumentRequest
    {
        public Guid InstrumentId { get; set; }
    }
}
