using System;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class PersonSearchResultDto
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string DisplayName { get; set; }
        public bool HasUser { get; set; }
        public Guid? UserId { get; set; }
    }
}
