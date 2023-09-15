using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class RemoveStakeholderGroupFromPerson
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid StakeholderGroupId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.StakeholderGroupId)
                    .MustAsync(async (command, stakeholderGroupId, cancellation) => await arpaContext.Set<PersonSection>()
                        .AnyAsync(ar => ar.SectionId == stakeholderGroupId && ar.PersonId == command.Id, cancellation))
                    .WithMessage("The stakeholder group is not linked to the person");
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                PersonSection personSectionToRemove = await _arpaContext.Set<PersonSection>()
                                    .FirstOrDefaultAsync(ar => ar.SectionId == request.StakeholderGroupId && ar.PersonId == request.Id, cancellationToken);

                _ = _arpaContext.Set<PersonSection>().Remove(personSectionToRemove);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _arpaContext.ClearChangeTracker();
                    return Unit.Value;
                }

                throw new AffectedRowCountMismatchException(nameof(PersonSection));
            }
        }
    }
}
