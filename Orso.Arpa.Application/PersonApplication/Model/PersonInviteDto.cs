using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class PersonInviteDto
    {
        public IList<Guid> PersonIds { get; set; } = [];
    }

    public class PersonInviteDtoValidator : AbstractValidator<PersonInviteDto>
    {
        public PersonInviteDtoValidator()
        {
            RuleFor(dto => dto.PersonIds).NotEmpty();

            RuleForEach(dto => dto.PersonIds).NotEmpty();
        }
    }

    public class PersonInviteDtoMappingProfile : Profile
    {
        public PersonInviteDtoMappingProfile()
        {
            CreateMap<PersonInviteDto, InvitePersonToApp.Command>();
        }
    }
}
