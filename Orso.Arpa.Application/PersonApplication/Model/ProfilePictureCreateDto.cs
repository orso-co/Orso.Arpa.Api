using System;
using System.IO;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class ProfilePictureCreateDto
    {
        [FromRoute]
        public Guid Id { get; set; }

        [FromForm]
        public IFormFile File { get; set; }
    }

    public class ProfilePictureCreateDtoMappingProfile : Profile
    {
        public ProfilePictureCreateDtoMappingProfile()
        {
            CreateMap<ProfilePictureCreateDto, UploadProfilePicture.Command>()
                .ForMember(cmd => cmd.PersonId, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(cmd => cmd.FormFile, opt => opt.MapFrom(dto => dto.File));
        }
    }

    public class ProfilePictureCreateDtoValidator : AbstractValidator<ProfilePictureCreateDto>
    {
        private const int MaxBytes = 5 * 1024 * 1024;

        private readonly string[] SupportedFileTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff" };

        private bool HasSupportedFileType(string fileName)
        {
            var fileType = Path.GetExtension(fileName).ToLower();
            return SupportedFileTypes.Contains(fileType);
        }

        public ProfilePictureCreateDtoValidator()
        {
            _ = RuleFor(dto => dto.Id).NotEmpty();

            _ = RuleFor(f => f.File)
                .NotNull()
                .WithMessage("Please attach your photo");

            _ = RuleFor(f => f.File.Length)
                .ExclusiveBetween(0, MaxBytes)
                .WithMessage($"File size should be greater than 0 and less than {MaxBytes / 1024 / 1024} MB")
                .When(f => f.File != null);

            _ = RuleFor(f => f.File.FileName)
                .NotEmpty()
                .Must(HasSupportedFileType)
                .WithMessage($"The provided file type is not supported. Supported file types are: {string.Join(", ", SupportedFileTypes)}")
                .When(f => f.File != null);
        }
    }
}
