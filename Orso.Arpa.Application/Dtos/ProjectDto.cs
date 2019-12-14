using System;

namespace Orso.Arpa.Application.Dtos
{
    public class ProjectDto : BaseEntityDto
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? GenreId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
