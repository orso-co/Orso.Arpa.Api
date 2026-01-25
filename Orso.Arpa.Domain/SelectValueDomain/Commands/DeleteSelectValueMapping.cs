using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Commands
{
    public static class DeleteSelectValueMapping
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Id)
                    .NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // Find the mapping
                var mapping = await _arpaContext.SelectValueMappings
                    .Include(m => m.SelectValue)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                if (mapping == null)
                {
                    throw new NotFoundException(nameof(SelectValueMapping), request.Id.ToString());
                }

                var selectValueId = mapping.SelectValueId;

                // Remove the mapping (soft delete)
                _arpaContext.SelectValueMappings.Remove(mapping);

                // Check if the SelectValue is used elsewhere
                var otherMappingsCount = await _arpaContext.SelectValueMappings
                    .CountAsync(m => m.SelectValueId == selectValueId && m.Id != request.Id, cancellationToken);

                // If the SelectValue is not used anywhere else, also remove it
                if (otherMappingsCount == 0)
                {
                    var selectValue = await _arpaContext.SelectValues
                        .FirstOrDefaultAsync(sv => sv.Id == selectValueId, cancellationToken);

                    if (selectValue != null)
                    {
                        _arpaContext.SelectValues.Remove(selectValue);
                    }
                }

                await _arpaContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
