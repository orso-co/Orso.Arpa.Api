using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.Projects
{
    public static class Modify
    {
        public class Command : IModifyCommand<Project>
        {
            public Guid Id { get; set; }

            public string Title { get; set; }
            public string ShortTitle { get; set; }
            public string Description { get; set; }
            public string Number { get; set; }
            public Guid? TypeId { get; set; }
            public Guid? GenreId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public virtual ICollection<Url> Urls { get; private set; } = new HashSet<Url>();
            public Guid? StateId { get; set; }
            public Guid? ParentId { get; set; }
            public bool IsCompleted { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Project>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.ShortTitle, opt => opt.MapFrom(src => src.ShortTitle))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                    .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                    // TODO .ForMember(dest => dest.Urls, opt => opt.MapFrom(src => src.Urls))
                    .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
                    .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                    .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(d => d.Id)
                    .EntityExists<Command, Project>(arpaContext);

                RuleFor(d => d.Number)
                    .MustAsync(async (dto, number, cancellation) =>
                        (!await arpaContext.Projects.AnyAsync(project => dto.Id != project.Id && project.Number.ToLower() == number.ToLower(), cancellation)))
                    .WithMessage("The specified project number is already in use. The project number needs to be unique.");
            }
        }
    }
}
