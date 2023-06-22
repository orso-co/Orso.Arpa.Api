using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Persons
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

                Person person = await _arpaContext.FindAsync<Person>(new object[] { request.PersonId }, cancellationToken);

                person.SetProfilePitureName(fileResult.Name);
                _ = _arpaContext.Persons.Update(person);

                return await _arpaContext.SaveChangesAsync(cancellationToken) <= 0
                    ? throw new Exception("Problem setting profile picture")
                    : fileResult;
            }
        }
    }
}
