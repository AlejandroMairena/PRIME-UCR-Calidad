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

        public string Decrypt(byte[] cipherText);

        public string FileToString(string path);
    }
}
