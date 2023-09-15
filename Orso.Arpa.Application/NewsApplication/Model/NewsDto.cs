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
}

public class NewsDtoMappingProfile : Profile
{
    public NewsDtoMappingProfile()
    {
        CreateMap<News, NewsDto>()
            .IncludeBase<BaseEntity, BaseEntityDto>();
    }
}
