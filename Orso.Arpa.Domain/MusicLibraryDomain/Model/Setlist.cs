using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Represents a setlist (program) containing a collection of music pieces.
    /// </summary>
    public class Setlist : BaseEntity
    {
        public Setlist(Guid? id, string name, string description, bool isTemplate) : base(id)
        {
            Name = name;
            Description = description;
            IsTemplate = isTemplate;
        }

        protected Setlist() { }

        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// If true, this setlist can be used as a template for creating new setlists.
        /// </summary>
        public bool IsTemplate { get; set; }

        public virtual ICollection<SetlistPiece> Pieces { get; set; } = new List<SetlistPiece>();
    }
}
