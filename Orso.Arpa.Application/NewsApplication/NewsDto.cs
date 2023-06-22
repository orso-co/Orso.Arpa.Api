using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.NewsApplication
{
    public class NewsDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
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
