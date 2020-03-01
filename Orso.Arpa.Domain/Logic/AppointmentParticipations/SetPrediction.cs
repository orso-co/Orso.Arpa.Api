using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.AppointmentParticipations
{
    public static class SetPrediction
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
            public Guid PredictionId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, AppointmentParticipation>()
                    .ForMember(dest => dest.PredictionId, opt => opt.MapFrom(src => src.PredictionId))
                    .ForAllOtherMembers(opt => opt.Ignore());

                CreateMap<Command, Create.Command>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.PredictionId, opt => opt.MapFrom(src => src.PredictionId))
                    .ForMember(dest => dest.ResultId, opt => opt.MapFrom(src => default(Guid?)));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IReadOnlyRepository readOnlyRepository)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(d => d.Id)
                    .MustAsync(async (id, cancellation) => await readOnlyRepository
                        .GetByIdAsync<Appointment>(id) != null)
                    .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
                RuleFor(d => d.PersonId)
                    .MustAsync(async (personId, cancellation) => await readOnlyRepository
                        .GetByIdAsync<Person>(personId) != null)
                    .OnFailure(dto => throw new RestException("Person not found", HttpStatusCode.NotFound, new { Person = "Not found" }));
                RuleFor(d => d.PredictionId)
                    .MustAsync(async (predictionId, cancellation) => await readOnlyRepository
                        .GetByIdAsync<SelectValueMapping>(predictionId) != null)
                    .OnFailure(dto => throw new RestException("Result not found", HttpStatusCode.NotFound, new { Result = "Not found" }));
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
                    _mapper.Map(request, participation);
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
