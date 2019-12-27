using System.Linq;
using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class RemoveProjectDtoValidator : AbstractValidator<RemoveProjectDto>
    {
        public RemoveProjectDtoValidator(IReadOnlyRepository readOnlyRepository)
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
                .MustAsync(async (dto, ProjectId, cancellation) => (await readOnlyRepository
                    .GetByIdAsync<Appointment>(dto.Id)).ProjectAppointments
                        .FirstOrDefault(ar => ar.ProjectId == ProjectId) != null)
                .WithMessage("The project is not linked to the appointment");
        }
    }
}
