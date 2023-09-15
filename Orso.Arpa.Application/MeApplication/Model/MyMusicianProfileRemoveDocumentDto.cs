using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;

namespace Orso.Arpa.Application.MeApplication.Model
{
    public class MyMusicianProfileRemoveDocumentDto
    {
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }
    }

    public class MyMusicianProfileRemoveDocumentDtoMappingProfile : Profile
    {
        public MyMusicianProfileRemoveDocumentDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileRemoveDocumentDto, RemoveDocumentFromMyMusicianProfile.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId));
        }
    }

    public class MyMusicianProfileRemoveDocumentDtoValidator : AbstractValidator<MyMusicianProfileRemoveDocumentDto>
    {
        public MyMusicianProfileRemoveDocumentDtoValidator()
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
