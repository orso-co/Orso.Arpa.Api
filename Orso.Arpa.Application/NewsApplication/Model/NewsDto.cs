using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.NewsDomain.Model;

namespace Orso.Arpa.Application.NewsApplication.Model;

public class NewsDto : BaseEntityDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public bool Show { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
}

public class NewsDtoMappingProfile : Profile
{
    public NewsDtoMappingProfile()
    {
        CreateMap<News, NewsDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>()
            .ForMember(dest => dest.IsRead, opt => opt.Ignore())
            .ForMember(dest => dest.ReadAt, opt => opt.Ignore());
    }
}
