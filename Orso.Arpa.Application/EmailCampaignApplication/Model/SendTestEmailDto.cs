using FluentValidation;

namespace Orso.Arpa.Application.EmailCampaignApplication.Model;

public class SendTestEmailDto
{
    public string EmailAddress { get; set; }
}

public class SendTestEmailDtoValidator : AbstractValidator<SendTestEmailDto>
{
    public SendTestEmailDtoValidator()
    {
        _ = RuleFor(d => d.EmailAddress)
            .NotEmpty()
            .EmailAddress();
    }
}
