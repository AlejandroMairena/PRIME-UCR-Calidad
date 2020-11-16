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
            //FilePath = Path.GetTempPath() + "/PRIME@UCR_Files"; //CAMBIAR PATH
            //FilePath = "C:/Users/gusta/Desktop/UCR/II SEMESTRE 2020/Proyecto integrador/PROYECTO/PRIME@UCR/wwwroot/data";
            //KeyString = key;
            FilePath = "wwwroot/datas/";
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
            ES.EncryptFile(path);
            //string keyString = System.Convert.ToBase64String(ES.Key);
            //string ivString = System.Convert.ToBase64String(ES.IV);
            //FilePath = "datas/";
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
