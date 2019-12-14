namespace Orso.Arpa.Application.Dtos
{
    public class PersonDto : BaseEntityDto
    {
        public string GivenName { get; set; }

        public string Surname { get; set; }
        public string RegisterName { get; set; }
    }
}
