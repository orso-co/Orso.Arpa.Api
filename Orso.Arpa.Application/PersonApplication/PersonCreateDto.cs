using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Persons.Create;

namespace Orso.Arpa.Application.PersonApplication
{
    public class PersonCreateDto
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
        public Guid GenderId { get; set; }
        public Guid? ContactViaId { get; set; }
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
                 .GeneralText(50);

            RuleFor(c => c.Surname)
                .NotEmpty()
                .GeneralText(50);

            RuleFor(c => c.AboutMe)
                .NotEmpty()
                .GeneralText(1000);

            RuleFor(c => c.GenderId)
                .NotEmpty();
        }
    }
}
