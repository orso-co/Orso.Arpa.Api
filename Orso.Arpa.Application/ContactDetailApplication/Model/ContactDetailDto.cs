using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.PersonDomain.Enums;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Application.ContactDetailApplication.Model
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
