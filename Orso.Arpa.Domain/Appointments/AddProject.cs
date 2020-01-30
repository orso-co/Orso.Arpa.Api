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

namespace Orso.Arpa.Domain.Appointments
{
    public static class AddProject
    {
        public class Command : IRequest
        {
            public Command(Guid id, Guid projectId)
            {
                Id = id;
                ProjectId = projectId;
            }

            public Guid Id { get; private set; }
            public Guid ProjectId { get; private set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, ProjectAppointment>()
                    .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IReadOnlyRepository readOnlyRepository)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;
                RuleFor(d => d.Id)
                    .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                    .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
                RuleFor(d => d.ProjectId)
                    .MustAsync(async (projectId, cancellation) => await readOnlyRepository.GetByIdAsync<Project>(projectId) != null)
                    .OnFailure(dto => throw new RestException("Project not found", HttpStatusCode.NotFound, new { Project = "Not found" }))
                    .MustAsync(async (dto, ProjectId, cancellation) => !(await readOnlyRepository
                        .GetByIdAsync<Appointment>(dto.Id)).ProjectAppointments
                            .Any(ar => ar.ProjectId == ProjectId))
                    .WithMessage("The project is already linked to the appointment");
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

                existingAppointment.ProjectAppointments.Add(_mapper.Map<ProjectAppointment>(request));

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
