using System;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class FileResult
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public string Extension { get; set; }
        public DateTimeOffset LastModified { get; set; }
    }
}
