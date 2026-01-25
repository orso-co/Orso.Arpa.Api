using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;

namespace Orso.Arpa.Domain.AppointmentDomain.Model
{
    /// <summary>
    /// Marks a setlist piece as prioritized/highlighted for a specific appointment (e.g., rehearsal focus).
    /// </summary>
    public class AppointmentSetlistPiece : BaseEntity
    {
        public AppointmentSetlistPiece(Guid? id, Guid appointmentId, Guid setlistPieceId) : base(id)
        {
            AppointmentId = appointmentId;
            SetlistPieceId = setlistPieceId;
        }

        protected AppointmentSetlistPiece() { }

        public Guid AppointmentId { get; private set; }
        public virtual Appointment Appointment { get; private set; }

        public Guid SetlistPieceId { get; private set; }
        public virtual SetlistPiece SetlistPiece { get; private set; }
    }
}
