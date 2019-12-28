using System.Linq;
using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class AddRegisterDtoValidator : AbstractValidator<AddRegisterDto>
    {
        public AddRegisterDtoValidator(IReadOnlyRepository readOnlyRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
            RuleFor(d => d.RegisterId)
                .NotEmpty()
                .MustAsync(async (registerId, cancellation) => await readOnlyRepository.GetByIdAsync<Section>(registerId) != null)
                .OnFailure(dto => throw new RestException("Register not found", HttpStatusCode.NotFound, new { Register = "Not found" }))
                .MustAsync(async (dto, RegisterId, cancellation) => !(await readOnlyRepository
                    .GetByIdAsync<Appointment>(dto.Id)).SectionAppointments
                        .Any(ar => ar.SectionId == RegisterId))
                .WithMessage("The register is already linked to the appointment");
        }
    }
}
