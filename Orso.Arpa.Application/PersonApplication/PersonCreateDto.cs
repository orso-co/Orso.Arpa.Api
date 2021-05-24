using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Persons.Create;

namespace Orso.Arpa.Application.PersonApplication
{
    public class PersonCreateDto
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
    }

    public class PersonCreateDtoMappingProfile : Profile
    {
        public PersonCreateDtoMappingProfile()
        {
            CreateMap<PersonCreateDto, Command>();
        }
    }

    public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
    {
        public PersonCreateDtoValidator()
        {
            RuleFor(c => c.GivenName)
                 .NotEmpty()
                 .MaximumLength(50);

            RuleFor(c => c.Surname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.AboutMe)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
