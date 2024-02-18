using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Queries
{
    public static class LoadProfilePicture
    {
        public class Query : IRequest<IFileResult>
        {
            public Query(Guid personId)
            {
                PersonId = personId;
            }

            public Guid PersonId { get; }
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
                var profilePictureFileName = (await _arpaContext.Set<Person>().FindAsync([request.PersonId], cancellationToken))?.ProfilePictureFileName;

                return profilePictureFileName is null
                    ? null
                    : await _fileAccessor.GetAsync(profilePictureFileName);
            }
        }
    }
}
