using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Defines which roles can access a specific music piece file
    /// </summary>
    public class MusicPieceFileRole : BaseEntity
    {
        public MusicPieceFileRole(Guid? id, Guid musicPieceFileId, Guid roleId) : base(id)
        {
            MusicPieceFileId = musicPieceFileId;
            RoleId = roleId;
        }

        public MusicPieceFileRole(MusicPieceFile musicPieceFile, Role role, Guid? id = null) : base(id)
        {
            MusicPieceFile = musicPieceFile;
            Role = role;
        }

        [JsonConstructor]
        protected MusicPieceFileRole()
        {
        }

        public Guid MusicPieceFileId { get; private set; }
        public virtual MusicPieceFile MusicPieceFile { get; private set; }

        public Guid RoleId { get; private set; }
        public virtual Role Role { get; private set; }
    }
}
