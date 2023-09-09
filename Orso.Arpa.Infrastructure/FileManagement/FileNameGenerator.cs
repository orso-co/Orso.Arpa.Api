using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Infrastructure.FileManagement
{
    public class FileNameGenerator : IFileNameGenerator
    {
        public string GenerateRandomFileName(IFormFile formFile)
        {
            string ext = Path.GetExtension(formFile.FileName);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
