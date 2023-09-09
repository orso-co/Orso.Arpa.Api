using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.AuditLogDomain.Enums;
using Orso.Arpa.Domain.AuditLogDomain.Model;

namespace Orso.Arpa.Application.AuditLogApplication.Model
{
    public class AuditLogDto
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuditLogType Type { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> OldValues { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; set; } = new Dictionary<string, object>();
        public IList<string> ChangedColumns { get; set; } = new List<string>();
    }

    public class AuditLogDtoMappingProfile : Profile
    {
        public AuditLogDtoMappingProfile()
        {
            CreateMap<AuditLog, AuditLogDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.TableName))

                .ForMember(dest => dest.OldValues, opt => opt.MapFrom(src => src.OldValues))
                .ForMember(dest => dest.NewValues, opt => opt.MapFrom(src => src.NewValues))
                .ForMember(dest => dest.ChangedColumns, opt => opt.MapFrom(src => src.ChangedColumns));
        }
    }
}
