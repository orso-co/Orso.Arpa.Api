using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.News.Create;

namespace Orso.Arpa.Application.NewsApplication
{

    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public bool Show { get; set; }
    }

    public class NewsCreateDtoMappingProfile : Profile
    {
        public NewsCreateDtoMappingProfile()
        {
            CreateMap<NewsCreateDto, Command>();
        }
    }

    public class NewsCreateDtoValidator : AbstractValidator<NewsCreateDto>
    {
        public NewsCreateDtoValidator()
        {
            RuleFor(c => c.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .RestrictedFreeText(200);
            RuleFor(c => c.Text)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .FreeText(1000);
            RuleFor(c => c.Url)
                .ValidUri(1000);

        }
    }
}
