using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetResult;

namespace Orso.Arpa.Application.AppointmentParticipationApplication
{
    public class AppointmentParticipationSetResultDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid ResultId { get; set; }
    }

    public class AppointmentParticipationSetResultDtoValidator : AbstractValidator<AppointmentParticipationSetResultDto>
    {
        public AppointmentParticipationSetResultDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.PersonId)
                .NotEmpty();
            RuleFor(d => d.ResultId)
                .NotEmpty();
        }
    }

    public class AppointmentParticipationSetResultDtoMappingProfile : Profile
    {
        public AppointmentParticipationSetResultDtoMappingProfile()
        {
            CreateMap<AppointmentParticipationSetResultDto, Command>();
        }
    }
}
