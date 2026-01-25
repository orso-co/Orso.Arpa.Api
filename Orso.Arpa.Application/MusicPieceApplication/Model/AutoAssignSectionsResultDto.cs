using System;
using System.Collections.Generic;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    public class AutoAssignSectionsResultDto
    {
        public int FilesProcessed { get; set; }
        public int FilesMatched { get; set; }
        public int SectionsAssigned { get; set; }
        public List<FileAssignmentDto> Assignments { get; set; } = new();
    }

    public class FileAssignmentDto
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public List<string> AssignedSections { get; set; } = new();
    }
}
