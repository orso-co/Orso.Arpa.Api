using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.MeApplication.Model
{
    public class SetMyAppointmentParticipationPredictionDto : IdFromRouteDto<SetMyAppointmentParticipationPredictionBodyDto>
    {
    }

    public class SetMyAppointmentParticipationPredictionBodyDto
    {
        public string CommentByPerformerInner { get; set; }
        public AppointmentParticipationPrediction Prediction { get; set; }
    }

    public class SetMyProjectAppointmentPredictionDtoValidator : IdFromRouteDtoValidator<SetMyAppointmentParticipationPredictionDto, SetMyAppointmentParticipationPredictionBodyDto>
    {
        public SetMyProjectAppointmentPredictionDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new SetMyAppointmentParticipationPredictionBodyDtoValidator());
        }
    }

    public class SetMyAppointmentParticipationPredictionBodyDtoValidator : AbstractValidator<SetMyAppointmentParticipationPredictionBodyDto>
    {
        public SetMyAppointmentParticipationPredictionBodyDtoValidator()
        {
            _ = RuleFor(d => d.CommentByPerformerInner)
                .RestrictedFreeText(500);
            _ = RuleFor(d => d.Prediction)
                .IsInEnum();
        }
    }

    public class SetMyProjectAppointmentPredictionDtoMappingProfile : Profile
    {
        public SetMyProjectAppointmentPredictionDtoMappingProfile()
        {
            _ = CreateMap<SetMyAppointmentParticipationPredictionDto, SetAppointmentParticipationPrediction.Command>()
                .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.Body.CommentByPerformerInner))
                .ForMember(dest => dest.Prediction, opt => opt.MapFrom(src => src.Body.Prediction));
        }
    }
}
