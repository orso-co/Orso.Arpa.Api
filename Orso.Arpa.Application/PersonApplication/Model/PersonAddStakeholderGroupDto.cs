using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.PersonDomain.Commands;

namespace Orso.Arpa.Application.PersonApplication.Model
{
    public class PersonAddStakeholderGroupDto
    {
        public Guid Id { get; set; }

        public Guid StakeholderGroupId { get; set; }
    }

    public class PersonAddStakeholderGroupDtoMappingProfile : Profile
    {
        public PersonAddStakeholderGroupDtoMappingProfile()
        {
            CreateMap<PersonAddStakeholderGroupDto, AddStakeholderGroup.Command>();
        }
    }

    public class PersonAddStakeholderGroupDtoValidator : AbstractValidator<PersonAddStakeholderGroupDto>
    {
        public PersonAddStakeholderGroupDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.StakeholderGroupId)
                .NotEmpty();
        }
    }
}
