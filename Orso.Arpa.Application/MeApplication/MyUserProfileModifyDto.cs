using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Me.Modify;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyUserProfileModifyDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }

        public string AboutMe { get; set; }
    }

    public class MyUserProfileModifyDtoMappingProfile : Profile
    {
        public MyUserProfileModifyDtoMappingProfile()
        {
            CreateMap<MyUserProfileModifyDto, Command>();
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
                .MaximumLength(50);

            RuleFor(c => c.Surname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.AboutMe)
                .MaximumLength(1000);
        }
    }
}
