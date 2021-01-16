using FluentValidation;

namespace Orso.Arpa.Application.AuthApplication
{
    public class ForgotPassswordDto
    {
        public string UserName { get; set; }
    }

    public class ForgotPassswordDtoValidator : AbstractValidator<ForgotPassswordDto>
    {
        public ForgotPassswordDtoValidator()
        {
            RuleFor(q => q.UserName)
                .NotEmpty();
        }
    }
}
