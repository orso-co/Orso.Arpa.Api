using System;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    public class MusicPieceFileAnnotation
    {
        public Guid Id { get; set; }
        public Guid MusicPieceFileId { get; set; }
        public Guid UserId { get; set; }
        public string AnnotationData { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
