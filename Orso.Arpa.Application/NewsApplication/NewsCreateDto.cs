using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.News.Create;

namespace Orso.Arpa.Application.NewsApplication
{

    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public bool Show { get; set; }
    }

    public class NewsCreateDtoMappingProfile : Profile
    {
        public NewsCreateDtoMappingProfile()
        {
            _ = CreateMap<NewsCreateDto, Command>();
        }
    }

    public class NewsCreateDtoValidator : AbstractValidator<NewsCreateDto>
    {
        public NewsCreateDtoValidator()
        {
            _ = RuleFor((c => c.Title))
                .NotEmpty()
                .FreeText(200);
            _ = RuleFor(c => c.Content)
                .NotEmpty()
                .FreeText(1000);
            _ = RuleFor(c => c.Url)
                .ValidUri(1000)
                .When(dto => !string.IsNullOrEmpty(dto.Url));

        }
    }
}
