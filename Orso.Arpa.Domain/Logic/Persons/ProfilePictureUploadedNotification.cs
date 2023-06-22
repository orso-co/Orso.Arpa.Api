using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Domain.Logic.Persons
{
    public class ProfilePictureUploadedNotification : INotification
    {
        public Guid PersonId { get; set; }
        public string OldFileName { get; set; }
        public string NewFileName { get; set; }
    }

    public class DeleteOldProfilePicture : INotificationHandler<ProfilePictureUploadedNotification>
    {
        private readonly IFileAccessor _fileAccessor;

        public DeleteOldProfilePicture(IFileAccessor fileAccessor)
        {
            _fileAccessor = fileAccessor;
        }

        public async Task Handle(ProfilePictureUploadedNotification notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(notification.OldFileName))
            {
                return;
            }
            await _fileAccessor.DeleteAsync(notification.OldFileName);
        }
    }
}
