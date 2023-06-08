using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Messages.Modify;

namespace Orso.Arpa.Application.MessageApplication
{
    public class MessageModifyDto : IdFromRouteDto<MessageModifyBodyDto>
    {
        [FromRoute]
        public Guid MessageId { get; set; }
    }

    public class MessageModifyBodyDto
    {
        public string MessageText { get; set; }
        public string Url { get; set; }
        public bool Show { get; set; }
    }

    public class MessageModifyDtoMappingProfile : Profile
    {
        public MessageModifyDtoMappingProfile()
        {
            CreateMap<MessageModifyDto, Command>()
                .ForMember(dest => dest.MessageText,
                    opt => opt.MapFrom(src => src.Body.MessageText))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Body.Url))
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Body.Show))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MessageId));
        }
    }
    public class MessageModifyDtoValidator : IdFromRouteDtoValidator<MessageModifyDto, MessageModifyBodyDto>
    {
        public MessageModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MessageModifyBodyDtoValidator());

            RuleFor(dto => dto.MessageId)
                .NotEmpty();
        }
    }

    public class MessageModifyBodyDtoValidator : AbstractValidator<MessageModifyBodyDto>
    {
        public MessageModifyBodyDtoValidator()
        {
            RuleFor(c => c.MessageText)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .RestrictedFreeText(500);

            RuleFor(c => c.Url)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ValidUri();
        }
    }
}
