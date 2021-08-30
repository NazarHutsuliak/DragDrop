using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DragDrop.Models
{
    public class MockFile : IFormFile
    {
        IFormFile file;

        public string ContentDisposition => file.ContentDisposition;

        public string ContentType => file.ContentType;

        public string FileName { get; set; }

        public IHeaderDictionary Headers => file.Headers;

        public long Length => file.Length;

        public string Name => file.Name;

        public void CopyTo(Stream target)
        {
            file.CopyTo(target);
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
           return file.CopyToAsync(target, cancellationToken);
        }

        public Stream OpenReadStream()
        {
            return file.OpenReadStream();
        }
    }
}
