using System;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class FileResult : IFileResult
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public string Extension { get; set; }
        public DateTimeOffset LastModified { get; set; }
    }
}
