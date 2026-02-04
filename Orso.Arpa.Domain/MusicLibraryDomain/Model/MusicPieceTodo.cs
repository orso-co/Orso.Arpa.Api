using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Model
{
    /// <summary>
    /// Represents a todo item associated with a music piece
    /// </summary>
    public class MusicPieceTodo : BaseEntity
    {
        public MusicPieceTodo(Guid? id, CreateMusicPieceTodo.Command command) : base(id)
        {
            MusicPieceId = command.MusicPieceId;
            Title = command.Title;
            DueDate = command.DueDate;
            IsCompleted = false;
        }

        [JsonConstructor]
        protected MusicPieceTodo()
        {
        }

        public void Update(ModifyMusicPieceTodo.Command command)
        {
            Title = command.Title;
            DueDate = command.DueDate;
        }

        public void ToggleCompletion()
        {
            IsCompleted = !IsCompleted;
            CompletedAt = IsCompleted ? DateTime.UtcNow : null;
        }

        /// <summary>
        /// Reference to the music piece
        /// </summary>
        public Guid MusicPieceId { get; private set; }
        public virtual MusicPiece MusicPiece { get; private set; }

        /// <summary>
        /// The todo title/description (max 500 characters)
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Optional due date
        /// </summary>
        public DateTime? DueDate { get; private set; }

        /// <summary>
        /// Whether the todo is completed
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// When the todo was completed
        /// </summary>
        public DateTime? CompletedAt { get; private set; }

        /// <summary>
        /// Users assigned to this todo
        /// </summary>
        public virtual ICollection<User> Assignees { get; private set; } = new HashSet<User>();

        public override string ToString()
        {
            return Title;
        }
    }
}
