using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Domain.PersonDomain.Queries
{
    public static class LoadProfilePicture
    {
        public class Query : IRequest<IFileResult>
        {
            public Query(Guid personId, bool loadOriginal = false)
            {
                PersonId = personId;
                LoadOriginal = loadOriginal;
            }

            public Guid PersonId { get; }
            public bool LoadOriginal { get; }
        }

        public class Handler : IRequestHandler<Query, IFileResult>
        {
            private readonly IArpaContext _arpaContext;
            private readonly IFileAccessor _fileAccessor;

            public Handler(IArpaContext arpaContext, IFileAccessor fileAccessor)
            {
                _arpaContext = arpaContext;
                _fileAccessor = fileAccessor;
            }

            async Task<IFileResult> IRequestHandler<Query, IFileResult>.Handle(Query request, CancellationToken cancellationToken)
            {
                var person = await _arpaContext.Persons.FindAsync(new object[] { request.PersonId }, cancellationToken);

                if (person is null)
                {
                    return null;
                }

                string fileName;

                if (request.LoadOriginal)
                {
                    fileName = person.OriginalProfilePictureFileName;
                }
                else
                {
                    fileName = person.ProfilePictureFileName;
                }

                return fileName is null
                    ? null
                    : await _fileAccessor.GetAsync(fileName);
            }
        }
    }
}
