using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Domain.Logic.MyContactDetails;

namespace Orso.Arpa.Application.MyContactDetailApplication
{
    public class MyContactDetailDeleteDto
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class MyContactDetailDeleteDtoMappingProfile : Profile
    {
        public MyContactDetailDeleteDtoMappingProfile()
        {
            CreateMap<MyContactDetailDeleteDto, Delete.Command>();
        }
    }

    public class MyContactDetailDeleteDtoValidator : AbstractValidator<MyContactDetailDeleteDto>
    {
        public MyContactDetailDeleteDtoValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty();
        }
    }
}
