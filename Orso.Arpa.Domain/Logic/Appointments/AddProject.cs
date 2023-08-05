using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
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
                    .EntityExists<Command, Appointment>(arpaContext);

                RuleFor(d => d.ProjectId)
                    .Cascade(CascadeMode.Stop)
                    .EntityExists<Command, Project>(arpaContext)

                    .MustAsync(async (dto, projectId, cancellation) => !(await arpaContext.ProjectAppointments
                        .AnyAsync(pa => pa.ProjectId == projectId && pa.AppointmentId == dto.Id, cancellation)))
                    .WithMessage("The project is already linked to the appointment");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(
                IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Appointment existingAppointment = await _arpaContext.Appointments.FindAsync(new object[] { request.Id }, cancellationToken);
                Project existingProject = await _arpaContext.Projects.FindAsync(new object[] { request.ProjectId }, cancellationToken);

                _arpaContext.ProjectAppointments.Add(new ProjectAppointment(null, existingProject, existingAppointment));

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(ProjectAppointment));
            }
        }
    }
}
