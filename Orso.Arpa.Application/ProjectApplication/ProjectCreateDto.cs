//using System;
//using AutoMapper;
//using FluentValidation;
//using Orso.Arpa.Application.Extensions;
//using static Orso.Arpa.Domain.Logic.Projects.AddUrl;

//namespace Orso.Arpa.Application.ProjectApplication
//{
//    public class ProjectAddUrlDto
//    {
//        public Guid Id { get; set; }
//        public string Href { get; set; }
//        public string AnchorText { get; set; }

//    }

//    public class ProjectCreateDtoMappingProfile : Profile
//    {
//        public ProjectAddUrlDtoMappingProfile()
//        {
//            CreateMap<ProjectAddUrlDto, Command>()
//                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
//                .ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Href))
//                .ForMember(dest => dest.AnchorText, opt => opt.MapFrom(src => src.AnchorText));
//        }
//    }

//    public class ProjectAddUrlDtoValidator : AbstractValidator<ProjectAddUrlDto>
//    {
//        public ProjectAddUrlDtoValidator()
//        {
//            RuleFor(p => p.Href)
//             .NotEmpty()
//             .ValidUri()
//             .MaximumLength(1000);

//            RuleFor(p => p.AnchorText)
//                .NotEmpty()
//                .MaximumLength(1000);

//            RuleFor(p => p.Id)
//                .NotEmpty();
//        }
//    }
//}
