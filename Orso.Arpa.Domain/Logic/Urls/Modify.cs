using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Urls
{
    public static class Modify
    {
        public class Command : IModifyCommand<Url>
        {
            public Guid Id { get; set; }
            public string Href { get; set; }
            public string AnchorText { get; set; }
            public virtual ICollection<UrlRole> UrlRoles { get; private set; } = new HashSet<UrlRole>();
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Url>()
                    .ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Href))
                    .ForMember(dest => dest.AnchorText, opt => opt.MapFrom(src => src.AnchorText))
                    .ForMember(dest => dest.UrlRoles, opt => opt.MapFrom(src => src.UrlRoles))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Url>(arpaContext);
            }
        }
    }
}
