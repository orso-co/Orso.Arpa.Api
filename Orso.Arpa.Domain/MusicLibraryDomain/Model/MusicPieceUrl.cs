using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Represents a URL associated with a music piece (YouTube, IMDB, Publisher, etc.)
    /// </summary>
    public class MusicPieceUrl : BaseEntity
    {
        public MusicPieceUrl(Guid? id, CreateMusicPieceUrl.Command command) : base(id)
        {
            MusicPieceId = command.MusicPieceId;
            Href = command.Href;
            AnchorText = command.AnchorText;
            UrlTypeId = command.UrlTypeId;
        }

        [JsonConstructor]
        protected MusicPieceUrl()
        {
        }

        public void Update(ModifyMusicPieceUrl.Command command)
        {
            Href = command.Href;
            AnchorText = command.AnchorText;
            UrlTypeId = command.UrlTypeId;
        }

        /// <summary>
        /// Reference to the music piece
        /// </summary>
        public Guid MusicPieceId { get; private set; }
        public virtual MusicPiece MusicPiece { get; private set; }

        /// <summary>
        /// The URL (max 1000 characters)
        /// </summary>
        public string Href { get; private set; }

        /// <summary>
        /// Display text for the link (max 200 characters)
        /// </summary>
        public string AnchorText { get; private set; }

        /// <summary>
        /// Type of URL (YouTube, Publisher, IMDB, etc.)
        /// </summary>
        public Guid? UrlTypeId { get; private set; }
        public virtual SelectValueMapping UrlType { get; private set; }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(AnchorText) ? Href : AnchorText;
        }
    }
}
