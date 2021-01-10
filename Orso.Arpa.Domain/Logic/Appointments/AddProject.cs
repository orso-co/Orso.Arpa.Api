using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Appointments
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

            public Command()
            {
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
            public Validator(IArpaContext arpaContext)
            {
                

                RuleFor(d => d.Id)
                    .EntityExists<Command, Appointment>(arpaContext, nameof(Command.Id));

                RuleFor(d => d.ProjectId)
                    .EntityExists<Command, Project>(arpaContext, nameof(Command.ProjectId))

                    .MustAsync(async (dto, projectId, cancellation) => !(await arpaContext.ProjectAppointments
                        .AnyAsync(pa => pa.ProjectId == projectId && pa.AppointmentId == dto.Id, cancellation)))
                    .WithMessage("The project is already linked to the appointment");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IMapper _mapper;

            public Handler(
                IArpaContext arpaContext,
                IMapper mapper)
            {
                _arpaContext = arpaContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(request.Id);

                existingAppointment.ProjectAppointments.Add(_mapper.Map<ProjectAppointment>(request));

                _arpaContext.Appointments.Update(existingAppointment);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating appointment");
            }
        }
    }
}
