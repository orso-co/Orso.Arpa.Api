using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class MusicPieceTodoModifyDto
    {
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public IList<Guid> AssigneeIds { get; set; } = new List<Guid>();
    }
}
