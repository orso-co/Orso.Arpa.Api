using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Commands
{
    public static class CreateSelectValueMapping
    {
        public class Command : IRequest<SelectValueMapping>
        {
            public string TableName { get; set; }
            public string PropertyName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.TableName)
                    .NotEmpty();

                RuleFor(c => c.PropertyName)
                    .NotEmpty();

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .MaximumLength(50);

                RuleFor(c => c)
                    .MustAsync(async (cmd, cancellation) =>
                    {
                        var category = await arpaContext.SelectValueCategories
                            .FirstOrDefaultAsync(c => c.Table == cmd.TableName && c.Property == cmd.PropertyName, cancellation);
                        return category != null;
                    })
                    .WithMessage("SelectValueCategory not found for the specified table and property");
            }
        }

        public class Handler : IRequestHandler<Command, SelectValueMapping>
        {
            private readonly IArpaContext _arpaContext;

            public Handler(IArpaContext arpaContext)
            {
                _arpaContext = arpaContext;
            }

            public async Task<SelectValueMapping> Handle(Command request, CancellationToken cancellationToken)
            {
                // Find the category
                var category = await _arpaContext.SelectValueCategories
                    .FirstOrDefaultAsync(c => c.Table == request.TableName && c.Property == request.PropertyName, cancellationToken);

                // Create new SelectValue
                var selectValue = new SelectValue(Guid.NewGuid(), request.Name, request.Description);
                _arpaContext.SelectValues.Add(selectValue);

                // Get max sort order for this category
                var maxSortOrder = await _arpaContext.SelectValueMappings
                    .Where(m => m.SelectValueCategoryId == category.Id)
                    .MaxAsync(m => (int?)m.SortOrder, cancellationToken) ?? 0;

                // Create mapping
                var mapping = new SelectValueMapping(Guid.NewGuid(), category.Id, selectValue.Id, maxSortOrder + 10);
                _arpaContext.SelectValueMappings.Add(mapping);

                await _arpaContext.SaveChangesAsync(cancellationToken);

                // Reload with navigation properties
                return await _arpaContext.SelectValueMappings
                    .Include(m => m.SelectValue)
                    .Include(m => m.SelectValueCategory)
                    .FirstAsync(m => m.Id == mapping.Id, cancellationToken);
            }
        }
    }
}
