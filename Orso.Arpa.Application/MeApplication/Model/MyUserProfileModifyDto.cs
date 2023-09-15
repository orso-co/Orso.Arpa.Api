using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.MeApplication.Model
{
    public class MyUserProfileModifyDto
    {
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
        public Guid GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Birthplace { get; set; }
        public string BirthName { get; set; }
    }

    public class MyUserProfileModifyDtoMappingProfile : Profile
    {
        public MyUserProfileModifyDtoMappingProfile()
        {
            CreateMap<MyUserProfileModifyDto, ModifyMyUser.Command>();
        }
    }

    public class MyUserProfileModifyDtoValidator : AbstractValidator<MyUserProfileModifyDto>
    {
        public MyUserProfileModifyDtoValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.GivenName)
                .NotEmpty()
                .PersonName(50);

            RuleFor(c => c.Surname)
                .NotEmpty()
                .PersonName(50);

            RuleFor(c => c.AboutMe)
                .RestrictedFreeText(1000);

            RuleFor(c => c.GenderId)
                .NotEmpty();

            RuleFor(c => c.Birthplace)
                .PlaceName(50);

            RuleFor(c => c.BirthName)
                .PersonName(50);
        }
    }
}
