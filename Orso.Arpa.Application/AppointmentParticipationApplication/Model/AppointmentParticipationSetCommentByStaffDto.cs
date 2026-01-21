using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.AppointmentParticipationApplication.Model
{
    public class AppointmentParticipationSetCommentByStaffDto : IdFromRouteDto<AppointmentParticipationSetCommentByStaffBodyDto>
    {
        [FromRoute]
        public Guid PersonId { get; set; }
    }

    public class AppointmentParticipationSetCommentByStaffBodyDto
    {
        public string CommentByStaffInner { get; set; }
    }

    public class AppointmentParticipationSetCommentByStaffDtoValidator : IdFromRouteDtoValidator<AppointmentParticipationSetCommentByStaffDto, AppointmentParticipationSetCommentByStaffBodyDto>
    {
        public AppointmentParticipationSetCommentByStaffDtoValidator()
        {
            _ = RuleFor(d => d.PersonId)
                .NotEmpty();
            _ = RuleFor(d => d.Body)
                .SetValidator(new AppointmentParticipationSetCommentByStaffBodyDtoValidator());
        }
    }

    public class AppointmentParticipationSetCommentByStaffBodyDtoValidator : AbstractValidator<AppointmentParticipationSetCommentByStaffBodyDto>
    {
        public AppointmentParticipationSetCommentByStaffBodyDtoValidator()
        {
            _ = RuleFor(d => d.CommentByStaffInner)
                .MaximumLength(500);
        }
    }

    public class AppointmentParticipationSetCommentByStaffDtoMappingProfile : Profile
    {
        public AppointmentParticipationSetCommentByStaffDtoMappingProfile()
        {
            _ = CreateMap<AppointmentParticipationSetCommentByStaffDto, SetAppointmentParticipationCommentByStaff.Command>()
                .ForMember(dest => dest.CommentByStaffInner, opt => opt.MapFrom(src => src.Body.CommentByStaffInner));
        }
    }
}
