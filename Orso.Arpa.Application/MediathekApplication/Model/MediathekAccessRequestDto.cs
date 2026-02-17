using System;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MediathekDomain.Enums;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Application.MediathekApplication.Model;

public class MediathekAccessRequestDto : BaseEntityDto
{
    public Guid PersonId { get; set; }
    public string PersonDisplayName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string MainInstrument { get; set; }
    public MediathekAccessRequestStatus Status { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string ProcessedBy { get; set; }
    public string Message { get; set; }
}

public class MediathekAccessRequestDtoMappingProfile : Profile
{
    public MediathekAccessRequestDtoMappingProfile()
    {
        CreateMap<MediathekAccessRequest, MediathekAccessRequestDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>()
            .ForMember(dest => dest.PersonDisplayName, opt => opt.MapFrom(src => src.Person.DisplayName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Person.User != null ? src.Person.User.UserName : null))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.User != null ? src.Person.User.Email : null))
            .ForMember(dest => dest.MainInstrument, opt => opt.MapFrom(src =>
                src.Person.MusicianProfiles != null && src.Person.MusicianProfiles.Count > 0
                    ? src.Person.MusicianProfiles.First().Instrument.Name
                    : null));
    }
}
