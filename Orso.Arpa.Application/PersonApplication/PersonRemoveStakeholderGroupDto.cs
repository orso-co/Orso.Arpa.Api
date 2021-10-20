using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Logic.Persons;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class PersonRemoveStakeholderGroupDto
    {
        public Guid Id { get; set; }

        public Guid StakeholderGroupId { get; set; }
    }

    public class PersonRemoveStakeholderGroupDtoMappingProfile : Profile
    {
        public PersonRemoveStakeholderGroupDtoMappingProfile()
        {
            CreateMap<PersonRemoveStakeholderGroupDto, RemoveStakeholderGroup.Command>();
        }
    }

    public class PersonRemoveStakeholderGroupDtoValidator : AbstractValidator<PersonRemoveStakeholderGroupDto>
    {
        public PersonRemoveStakeholderGroupDtoValidator()
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
