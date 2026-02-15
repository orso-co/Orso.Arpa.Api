using System;
using AutoMapper;
using Orso.Arpa.Domain.InstrumentationDomain.Commands;

namespace Orso.Arpa.Application.InstrumentationApplication.Model
{
    public class InstrumentationCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsTemplate { get; set; }
        public Guid? ProjectId { get; set; }
    }

    public class InstrumentationCreateDtoMappingProfile : Profile
    {
        public InstrumentationCreateDtoMappingProfile()
        {
            CreateMap<InstrumentationCreateDto, CreateInstrumentation.Command>();
        }
    }
}
