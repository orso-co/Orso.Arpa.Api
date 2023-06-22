using System;

namespace Orso.Arpa.Domain.Interfaces
{
    public interface IFileResult
    {
        byte[] Content { get; set; }
        string ContentType { get; set; }
        string Extension { get; set; }
        DateTimeOffset LastModified { get; set; }
        string Name { get; set; }
    }
}
