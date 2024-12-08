using System.Collections.Generic;

namespace Orso.Arpa.Application.MyProjectApplication.Model
{
    public class MyProjectListDto
    {
        public IList<MyProjectDto> UserProjects { get; set; } = [];
        public int TotalRecordsCount { get; set; }
    }
}
