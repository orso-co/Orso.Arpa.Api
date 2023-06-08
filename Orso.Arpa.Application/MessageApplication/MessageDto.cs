using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MessageApplication
{
    public class MessageDto : BaseEntityDto
    {
        public string MessageText { get; set; }
        public string Url { get; set; }
        public bool Show { get; set; }
    }

    public class MessageDtoMappingProfile : Profile
    {
        public MessageDtoMappingProfile()
        {
            CreateMap<Message, MessageDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
