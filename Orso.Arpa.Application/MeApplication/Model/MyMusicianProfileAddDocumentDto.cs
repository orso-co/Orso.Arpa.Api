using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;

namespace Orso.Arpa.Application.MeApplication.Model
{
    public class MyMusicianProfileAddDocumentDto
    {
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }
    }

    public class MyMusicianProfileAddDocumentDtoMappingProfile : Profile
    {
        public MyMusicianProfileAddDocumentDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileAddDocumentDto, AddDocumentToMyMusicianProfile.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId));
        }
    }

    public class MyMusicianProfileAddDocumentDtoValidator : AbstractValidator<MyMusicianProfileAddDocumentDto>
    {
        public MyMusicianProfileAddDocumentDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.DocumentId)
                .NotEmpty();
        }
    }
}
