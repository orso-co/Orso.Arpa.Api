using System.Linq;
using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class AddSectionDtoValidator : AbstractValidator<AddSectionDto>
    {
        public AddSectionDtoValidator(IReadOnlyRepository readOnlyRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
            RuleFor(d => d.SectionId)
                .NotEmpty()
                .MustAsync(async (sectionId, cancellation) => await readOnlyRepository.GetByIdAsync<Section>(sectionId) != null)
                .OnFailure(dto => throw new RestException("Section not found", HttpStatusCode.NotFound, new { Section = "Not found" }))
                .MustAsync(async (dto, sectionId, cancellation) => !(await readOnlyRepository
                    .GetByIdAsync<Appointment>(dto.Id)).SectionAppointments
                        .Any(ar => ar.SectionId == sectionId))
                .WithMessage("The section is already linked to the appointment");
        }
    }
}
