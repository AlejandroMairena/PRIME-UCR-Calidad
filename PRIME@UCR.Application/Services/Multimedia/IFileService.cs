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
        public string KeyString { get; set; }
        public string IVString { get; set; }
        public Task<bool> StoreFile(string path, string fileName,string extension, Stream fileStream);
        public Task<string> StoreTextFile(string text, string fileName);
        public bool DeleteFile(string filePath);

        public void SetKeyIV(byte[] iv, byte[] key);
    }
}
