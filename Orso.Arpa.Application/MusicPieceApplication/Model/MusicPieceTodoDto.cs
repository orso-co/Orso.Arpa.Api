using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicLibraryDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Application.MusicPieceApplication.Model
{
    /// <summary>
    /// Simplified user DTO for todo assignees (no navigation properties needed)
    /// </summary>
    public class TodoAssigneeDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }

    public class MusicPieceTodoDto : BaseEntityDto
    {
        public Guid MusicPieceId { get; set; }
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public IList<TodoAssigneeDto> Assignees { get; set; } = new List<TodoAssigneeDto>();
    }

    public class MusicPieceTodoDtoMappingProfile : Profile
    {
        public MusicPieceTodoDtoMappingProfile()
        {
            _ = CreateMap<User, TodoAssigneeDto>();

            _ = CreateMap<MusicPieceTodo, MusicPieceTodoDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
