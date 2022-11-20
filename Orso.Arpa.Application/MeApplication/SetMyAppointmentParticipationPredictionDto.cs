using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using static Orso.Arpa.Domain.Logic.AppointmentParticipations.SetPrediction;

namespace Orso.Arpa.Application.MeApplication
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
            _ = CreateMap<SetMyAppointmentParticipationPredictionDto, Command>()
                .ForMember(dest => dest.CommentByPerformerInner, opt => opt.MapFrom(src => src.Body.CommentByPerformerInner))
                .ForMember(dest => dest.Prediction, opt => opt.MapFrom(src => src.Body.Prediction));
        }
    }
}
