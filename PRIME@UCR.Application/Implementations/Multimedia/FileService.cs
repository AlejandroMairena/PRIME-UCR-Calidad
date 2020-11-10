using PRIME_UCR.Application.Services.Multimedia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Implementations.Multimedia
{
    public class FileService : IFileService
    {
        public string FilePath { get; set; }
        public string KeyString { get; set; }
        public string IVString { get; set; }
        public EncryptionService ES { get; set; }

        public FileService()
        {
            FilePath = Path.GetTempPath() + "\\PRIME@UCR_Files"; //CAMBIAR PATH
            //KeyString = key;
            //IVString = iv;
            ES = new EncryptionService();
        }

        public async Task<bool> StoreFile(string fileName, Stream fileStream)
        {
            DirectoryInfo info = new DirectoryInfo(FilePath);
         
            if (!info.Exists) info.Create();

            string path = Path.Combine(FilePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                await fileStream.CopyToAsync(outputFileStream);
            }
            ES.EncryptFile(FilePath, fileName);
            //string keyString = System.Convert.ToBase64String(ES.Key);
            //string ivString = System.Convert.ToBase64String(ES.IV);
            return true;
        }
        public void SetKeyIV(byte[] iv, byte[] key) {
            ES.SetKeyIV(iv, key);
        }
    }
}
