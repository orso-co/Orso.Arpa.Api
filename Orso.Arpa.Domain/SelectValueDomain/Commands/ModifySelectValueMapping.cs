using System;
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
    public static class ModifySelectValueMapping
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
            public string TableName { get; set; }
            public string PropertyName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Id)
                    .NotEmpty();

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(50);
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

                // Update the SelectValue (not the mapping)
                var selectValue = mapping.SelectValue;

                // Use reflection to set private setters
                typeof(SelectValue).GetProperty(nameof(SelectValue.Name))?.SetValue(selectValue, request.Name);
                typeof(SelectValue).GetProperty(nameof(SelectValue.Description))?.SetValue(selectValue, request.Description);

                _arpaContext.SelectValues.Update(selectValue);

                await _arpaContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
