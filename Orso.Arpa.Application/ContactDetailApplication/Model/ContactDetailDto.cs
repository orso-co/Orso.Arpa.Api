using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Application.ContactDetailApplication
{
    public class ContactDetailDto : BaseEntityDto
    {
        public ContactDetailKey Key { get; set; }
        public string Value { get; set; }
        public Guid? TypeId { get; set; }
        public string CommentInner { get; set; }
        public string CommentTeam { get; set; }
        public byte Preference { get; set; }
    }

    public class ContactDetailDtoMappingProfile : Profile
    {
        public ContactDetailDtoMappingProfile()
        {
            CreateMap<ContactDetail, ContactDetailDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
