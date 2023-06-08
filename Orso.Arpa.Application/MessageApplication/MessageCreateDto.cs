using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Messages.Create;

namespace Orso.Arpa.Application.MessageApplication
{

    public class MessageCreateDto : IdFromRouteDto<MessageCreateBodyDto>
    {
    }

    public class MessageCreateBodyDto
    {
        public string MessageText { get; set; }
        public string Url { get; set; }
        public bool Show { get; set; }
    }

    public class MessageCreateDtoMappingProfile : Profile
    {
        public MessageCreateDtoMappingProfile()
        {
            CreateMap<MessageCreateDto, Command>()
                .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.Body.MessageText))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Body.Url))
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Body.Show));
        }
    }

    public class
        MessageCreateDtoValidator : IdFromRouteDtoValidator<MessageCreateDto, MessageCreateBodyDto>
    {
        public MessageCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MessageCreateBodyDtoValidator());
        }
    }

    public class MessageCreateBodyDtoValidator : AbstractValidator<MessageCreateBodyDto>
    {
        public MessageCreateBodyDtoValidator()
        {
            RuleFor(c => c.MessageText)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .RestrictedFreeText(500);
        }
    }
}
