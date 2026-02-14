using System;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.TodoDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.TodoDomain.Model
{
    public class TodoComment : BaseEntity
    {
        public TodoComment(Guid? id, CreateTodoComment.Command command) : base(id)
        {
            TodoItemId = command.TodoItemId;
            AuthorId = command.AuthorId;
            Content = command.Content;
            PostedAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        protected TodoComment()
        {
        }

        public Guid TodoItemId { get; private set; }
        public virtual TodoItem TodoItem { get; private set; }

        public Guid AuthorId { get; private set; }
        public virtual User Author { get; private set; }

        public string Content { get; private set; }
        public DateTime PostedAt { get; private set; }
    }
}
