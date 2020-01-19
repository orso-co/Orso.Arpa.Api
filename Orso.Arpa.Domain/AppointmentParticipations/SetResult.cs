using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.AppointmentParticipations
{
    public static class SetResult
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public Guid ResultId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, AppointmentParticipation>()
                    .ForMember(dest => dest.ResultId, opt => opt.MapFrom(src => src.ResultId))
                    .ForAllOtherMembers(opt => opt.Ignore());

                CreateMap<Command, Create.Command>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.PredictionId, opt => opt.MapFrom(src => default(Guid?)))
                    .ForMember(dest => dest.ResultId, opt => opt.MapFrom(src => src.ResultId));
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository _repository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(
                IRepository repository,
                IUnitOfWork unitOfWork,
                IMapper mapper)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _repository.GetByIdAsync<Appointment>(request.Id);
                AppointmentParticipation participation = existingAppointment.AppointmentParticipations
                    .FirstOrDefault(pa => pa.PersonId == request.PersonId);

                if (participation == null)
                {
                    participation = new AppointmentParticipation(Guid.NewGuid(), _mapper.Map<Command, Create.Command>(request));
                    await _repository.AddAsync(participation);
                }
                else
                {
                    _mapper.Map<Command, AppointmentParticipation>(request, participation);
                    _repository.Update(participation);
                }

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment participation");
            }
        }
    }
}
