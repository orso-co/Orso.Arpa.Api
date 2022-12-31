using System;

namespace Orso.Arpa.Application.AppointmentParticipationApplication
{
    public class AppointmentParticipationSetResultDto : IdFromRouteDto<AppointmentParticipationSetResultBodyDto>
    {
        [FromRoute]
        public Guid PersonId { get; set; }
    }

    public class AppointmentParticipationSetResultBodyDto
    {
        public AppointmentParticipationResult Result { get; set; }
    }

    public class AppointmentParticipationSetResultDtoValidator : IdFromRouteDtoValidator<AppointmentParticipationSetResultDto, AppointmentParticipationSetResultBodyDto>
    {
        public AppointmentParticipationSetResultDtoValidator()
        {
            _ = RuleFor(d => d.PersonId)
                .NotEmpty();
            _ = RuleFor(d => d.Body)
                .SetValidator(new AppointmentParticipationSetResultBodyDtoValidator());
        }
    }

    public class AppointmentParticipationSetResultBodyDtoValidator : AbstractValidator<AppointmentParticipationSetResultBodyDto>
    {
        public AppointmentParticipationSetResultBodyDtoValidator()
        {
            _ = RuleFor(d => d.Result)
                .IsInEnum();
        }
    }

    public class AppointmentParticipationSetResultDtoMappingProfile : Profile
    {
        public AppointmentParticipationSetResultDtoMappingProfile()
        {
            _ = CreateMap<AppointmentParticipationSetResultDto, Command>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Body.Result));
        }
    }
}
