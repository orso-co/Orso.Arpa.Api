using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.TodoDomain.Model;

namespace Orso.Arpa.Application.TodoApplication.Model
{
    public class TodoCommentDto : BaseEntityDto
    {
        public Guid TodoItemId { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorDisplayName { get; set; }
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
    }

    public class TodoCommentDtoMappingProfile : Profile
    {
        public TodoCommentDtoMappingProfile()
        {
            _ = CreateMap<TodoComment, TodoCommentDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.AuthorDisplayName, opt => opt.MapFrom(src =>
                    src.Author != null && src.Author.Person != null
                        ? src.Author.Person.GivenName + " " + src.Author.Person.Surname
                        : null));
        }
    }
}
