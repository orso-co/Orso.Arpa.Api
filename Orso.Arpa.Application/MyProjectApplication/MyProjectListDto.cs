using System.Collections.Generic;

namespace Orso.Arpa.Application.MyProjectApplication
{
    public class MyProjectListDto
    {
        public IList<MyProjectDto> UserProjects { get; set; } = new List<MyProjectDto>();
        public int TotalRecordsCount { get; set; }
    }
}
