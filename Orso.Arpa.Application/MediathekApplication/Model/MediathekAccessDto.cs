using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Application.MediathekApplication.Model;

public class MediathekAccessDto : BaseEntityDto
{
    public Guid PersonId { get; set; }
    public string PersonDisplayName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string GrantedBy { get; set; }
    public DateTime GrantedAt { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
}

public class MediathekAccessDtoMappingProfile : Profile
{
    public MediathekAccessDtoMappingProfile()
    {
        CreateMap<MediathekAccess, MediathekAccessDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>()
            .ForMember(dest => dest.PersonDisplayName, opt => opt.MapFrom(src => src.Person.DisplayName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Person.User != null ? src.Person.User.UserName : null))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.User != null ? src.Person.User.Email : null));
    }
}
