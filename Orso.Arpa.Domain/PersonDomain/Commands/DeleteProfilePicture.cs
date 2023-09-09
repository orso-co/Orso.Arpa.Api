using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class DeleteProfilePicture
    {
        public class Command : IRequest
        {
            public Command(Guid personId)
            {
                PersonId = personId;
            }

            public Guid PersonId { get; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.PersonId)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IFileAccessor _fileAccessor;
            private readonly IArpaContext _arpaContext;

            public Handler(IFileAccessor fileAccessor, IArpaContext arpaContext)
            {
                _fileAccessor = fileAccessor;
                _arpaContext = arpaContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Person person = await _arpaContext.FindAsync<Person>(new object[] { request.PersonId }, cancellationToken);
                var profilePictureFileName = person.ProfilePictureFileName;

                if (string.IsNullOrWhiteSpace(profilePictureFileName))
                {
                    return Unit.Value;
                }

                person.SetProfilePitureName(null);
                _ = _arpaContext.Persons.Update(person);

                if (await _arpaContext.SaveChangesAsync(cancellationToken) < 1)
                {
                    throw new AffectedRowCountMismatchException(nameof(Person));
                }

                await _fileAccessor.DeleteAsync(profilePictureFileName);

                return Unit.Value;
            }
        }
    }
}
