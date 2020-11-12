using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.Multimedia
{
    public interface IFileService
    {
        public string FilePath { get; set; }

        public Task<bool> StoreFile(string fileName, Stream fileStream);
        public bool StoreFile(string filePath);

    }
}
