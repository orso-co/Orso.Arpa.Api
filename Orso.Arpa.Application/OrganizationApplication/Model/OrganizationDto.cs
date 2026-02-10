using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.OrganizationDomain.Model;

namespace Orso.Arpa.Application.OrganizationApplication.Model
{
    public class OrganizationDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public SelectValueDto LegalForm { get; set; }
        public SelectValueDto OrganizationType { get; set; }
        public IList<string> Tags { get; set; } = [];
        public int PersonOrganizationCount { get; set; }
        public int OrganizationRelationshipCount { get; set; }
        public string DisplayName { get; set; }
    }

    public class OrganizationDtoMappingProfile : Profile
    {
        public OrganizationDtoMappingProfile()
        {
            _ = CreateMap<Organization, OrganizationDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Tags)
                        ? new List<string>()
                        : src.Tags.Split(',', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries).ToList()))
                .ForMember(dest => dest.PersonOrganizationCount, opt => opt.MapFrom(src => src.PersonOrganizations.Count))
                .ForMember(dest => dest.OrganizationRelationshipCount, opt => opt.MapFrom(src =>
                    src.RelationshipsAsSource.Count + src.RelationshipsAsTarget.Count))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
