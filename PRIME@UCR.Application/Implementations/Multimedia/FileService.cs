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
            FilePath = "wwwroot/datas/";
            ES = new EncryptionService();
        }
        //00EIWb1ubBfgsk9el1G2tBsJ1fRX0DGsD53xrHcClP1jMDHK+z2qyLwUG2-uMV9ZHZapHnuKj95-mWYuCwqBVA==.mp3
        public async Task<bool> StoreFile(string pathDecrypted, string fileName, string extension, Stream fileStream)
        {
            DirectoryInfo info = new DirectoryInfo(pathDecrypted);
            if (!info.Exists) {
                info.Create();
            }
            string completeFileName = fileName + extension;
            string path = Path.Combine(pathDecrypted, completeFileName); //wwwroot/asdkjbaskdn/ljasndljna/kajsndf.jpg
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                await fileStream.CopyToAsync(outputFileStream);
            }
            ES.EncryptFile(path);

            return true;
        }

        public async Task<string> StoreTextFile(string text, string fileName)
        {
            string path = Path.Combine(FilePath, fileName);
            using (StreamWriter sw = File.CreateText(path))
            {
                await sw.WriteLineAsync(text);
            }
            return path;
        }

        public void SetKeyIV(byte[] iv, byte[] key) {
            ES.SetKeyIV(iv, key);
        }

        public bool DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
