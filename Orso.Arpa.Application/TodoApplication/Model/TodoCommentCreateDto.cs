using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.TodoDomain.Commands;

namespace Orso.Arpa.Application.TodoApplication.Model
{
    public class TodoCommentCreateDto
    {
        public string Content { get; set; }
    }

    public class TodoCommentCreateDtoMappingProfile : Profile
    {
        public TodoCommentCreateDtoMappingProfile()
        {
            _ = CreateMap<TodoCommentCreateDto, CreateTodoComment.Command>();
        }
    }

    public class TodoCommentCreateDtoValidator : AbstractValidator<TodoCommentCreateDto>
    {
        public TodoCommentCreateDtoValidator()
        {
            _ = RuleFor(p => p.Content)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
