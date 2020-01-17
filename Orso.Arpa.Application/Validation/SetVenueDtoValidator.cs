using System.Net;
using FluentValidation;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Validation
{
    public class SetVenueDtoValidator : AbstractValidator<SetVenueDto>
    {
        public SetVenueDtoValidator(IReadOnlyRepository readOnlyRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellation) => await readOnlyRepository.GetByIdAsync<Appointment>(id) != null)
                .OnFailure(dto => throw new RestException("Appointment not found", HttpStatusCode.NotFound, new { Appointment = "Not found" }));
            RuleFor(d => d.VenueId)
                .MustAsync(async (venueId, cancellation) => venueId == null || await readOnlyRepository.GetByIdAsync<Venue>(venueId.Value) != null)
                .OnFailure(dto => throw new RestException("Venue not found", HttpStatusCode.NotFound, new { Venue = "Not found" }));
        }
    }
}
