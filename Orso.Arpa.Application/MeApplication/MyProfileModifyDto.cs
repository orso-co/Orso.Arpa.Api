using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Me.Modify;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyProfileModifyDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }

    public class MyProfileModifyDtoMappingProfile : Profile
    {
        public MyProfileModifyDtoMappingProfile()
        {
            CreateMap<MyProfileModifyDto, Command>();
        }
    }

    public class MyProfileModifyDtoValidator : AbstractValidator<MyProfileModifyDto>
    {
        public MyProfileModifyDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(c => c.GivenName)
                .NotEmpty();
            RuleFor(c => c.Surname)
                .NotEmpty();
        }
    }
}
