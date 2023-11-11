using System;
using AutoMapper;
using Orso.Arpa.Domain.General.Configuration;

namespace Orso.Arpa.Application.ClubApplication.Model
{
    public class ClubDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContactEmail { get; set; }
        public Uri Url { get; set; }
    }

    public class ClubDtoMappingProfile : Profile
    {
        public ClubDtoMappingProfile()
        {
            CreateMap<ClubConfiguration, ClubDto>();
        }
    }
}