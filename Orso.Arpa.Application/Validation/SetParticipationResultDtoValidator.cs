using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class SetParticipationResultDtoValidator : AbstractValidator<SetParticipationResultDto>
    {
        public SetParticipationResultDtoValidator(IReadOnlyRepository readOnlyRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellation) => await readOnlyRepository
                    .GetByIdAsync<Appointment>(id) != null)
                .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
            RuleFor(d => d.PersonId)
                .NotEmpty()
                .MustAsync(async (personId, cancellation) => (await readOnlyRepository
                    .GetByIdAsync<Person>(personId)) != null)
                .OnFailure(dto => throw new RestException("Person not found", HttpStatusCode.NotFound, new { Person = "Not found" }));
            RuleFor(d => d.ResultId)
                .NotEmpty()
                .MustAsync(async (resultId, cancellation) => await readOnlyRepository
                    .GetByIdAsync<SelectValueMapping>(resultId) != null)
                .OnFailure(dto => throw new RestException("Result not found", HttpStatusCode.NotFound, new { Result = "Not found" }));
        }
    }
}
