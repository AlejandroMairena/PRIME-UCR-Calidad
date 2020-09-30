using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Services.Multimedia
{
    public interface IEncryptionService
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public byte[] Encrypt(string plainText);
        public bool EncryptFile(string path, string fileName);
        public bool ByteArrayToFile(string filePath, byte[] byteArray);
        public string Decrypt(byte[] cipherText);

        public string FiletoString(string path);
    }
}
