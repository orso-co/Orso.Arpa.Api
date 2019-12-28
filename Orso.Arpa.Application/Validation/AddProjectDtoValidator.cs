using System.Linq;
using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class AddProjectDtoValidator : AbstractValidator<AddProjectDto>
    {
        public AddProjectDtoValidator(IReadOnlyRepository readOnlyRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
            RuleFor(d => d.ProjectId)
                .NotEmpty()
                .MustAsync(async (projectId, cancellation) => await readOnlyRepository.GetByIdAsync<Project>(projectId) != null)
                .OnFailure(dto => throw new RestException("Project not found", HttpStatusCode.NotFound, new { Project = "Not found" }))
                .MustAsync(async (dto, ProjectId, cancellation) => !(await readOnlyRepository
                    .GetByIdAsync<Appointment>(dto.Id)).ProjectAppointments
                        .Any(ar => ar.ProjectId == ProjectId))
                .WithMessage("The project is already linked to the appointment");
        }
    }
}
