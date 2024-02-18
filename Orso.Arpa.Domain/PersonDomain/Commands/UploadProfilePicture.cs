using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Domain.General.Extensions;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Commands
{
    public static class UploadProfilePicture
    {
        public class Command : IRequest<IFileResult>
        {
            public Guid PersonId { get; set; }
            public IFormFile FormFile { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                _ = RuleFor(d => d.PersonId)
                    .EntityExists<Command, Person>(arpaContext);
            }
        }

        public class Handler : IRequestHandler<Command, IFileResult>
        {
            private readonly IFileNameGenerator _fileNameGenerator;
            private readonly IFileAccessor _fileAccessor;
            private readonly IArpaContext _arpaContext;

            public Handler(IFileNameGenerator fileNameGenerator, IFileAccessor fileAccessor, IArpaContext arpaContext)
            {
                _fileNameGenerator = fileNameGenerator;
                _fileAccessor = fileAccessor;
                _arpaContext = arpaContext;
            }

            public async Task<IFileResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var fileName = _fileNameGenerator.GenerateRandomFileName(request.FormFile);
                IFileResult fileResult = await _fileAccessor.SaveAsync(request.FormFile, fileName);

                Person person = await _arpaContext.FindAsync<Person>([request.PersonId], cancellationToken);

                person.SetProfilePitureName(fileResult.Name);
                _ = _arpaContext.Set<Person>().Update(person);

                return await _arpaContext.SaveChangesAsync(cancellationToken) < 1
                    ? throw new AffectedRowCountMismatchException(nameof(Person))
                    : fileResult;
            }
        }
    }
}
