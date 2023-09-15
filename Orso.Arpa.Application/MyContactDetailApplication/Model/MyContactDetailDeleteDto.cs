using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.MyContactDetailApplication.Model
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
            CreateMap<MyContactDetailDeleteDto, DeleteMyContactDetail.Command>();
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
