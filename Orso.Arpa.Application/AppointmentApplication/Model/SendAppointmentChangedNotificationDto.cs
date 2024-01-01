using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class SendAppointmentChangedNotificationDto
    {
        [FromRoute]
        public Guid Id { get; set; }
        
        [FromQuery]
        public bool ForceSending { get; set; }
    }

    public class SendAppointmentChangedNotificationDtoMappingProfile : Profile
    {
        public SendAppointmentChangedNotificationDtoMappingProfile()
        {
            CreateMap<SendAppointmentChangedNotificationDto, SendAppointmentChangedNotification.Command>()
                .ForMember(d => d.AppointmentId, o => o.MapFrom(s => s.Id));
        }
    }

    public class SendAppointmentChangedNotificationDtoValidator : AbstractValidator<SendAppointmentChangedNotificationDto>
    {
        public SendAppointmentChangedNotificationDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
        }
    }
}