using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Persons.Invite;

namespace Orso.Arpa.Application.PersonApplication
{
    public class PersonInviteDto
    {
        public IList<Guid> PersonIds { get; set; } = new List<Guid>();
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
            CreateMap<PersonInviteDto, Command>();
        }
    }
}
