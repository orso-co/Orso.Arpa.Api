using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.NewsApplication
{
    public class NewsDto : BaseEntityDto
    {
        public string NewsText { get; set; }
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
}
