using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Appointments
{
    public static class SetDates
    {
        public class Command : IRequest
        {
            public Guid Id { get; private set; }
            public DateTime? StartTime { get; private set; }
            public DateTime? EndTime { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, Appointment>()
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom((src, dest) => src.StartTime ?? dest.StartTime))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom((src, dest) => src.EndTime ?? dest.EndTime))
                    .ForAllOtherMembers(opt => opt.Ignore());
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

                if (request.StartTime == null && request.EndTime == null)
                {
                    return Unit.Value;
                }

                _mapper.Map<Command, Appointment>(request, existingAppointment);

                if (existingAppointment.EndTime < existingAppointment.StartTime)
                {
                    throw new RestException("EndTime must be greater than StartTime", HttpStatusCode.BadRequest, new { EndTime = "is before StartTime" });
                }

                _repository.Update(existingAppointment);

                if (await _unitOfWork.CommitAsync())
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}
