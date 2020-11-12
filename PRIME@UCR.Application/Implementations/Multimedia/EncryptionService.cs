﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Application.Services.Multimedia;

namespace PRIME_UCR.Application.Implementations.Multimedia
{
    public class EncryptionService : IEncryptionService
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public EncryptionService() {
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                myRijndael.GenerateKey();
                myRijndael.GenerateIV();
                Key = myRijndael.Key;
                IV = myRijndael.IV;
            }
        }
        public string FiletoString(string path) {
            string fileString = "";
            if (File.Exists(path))
            {
                fileString = File.ReadAllText(path);
            }
            return fileString;
        }

        public bool ByteArrayToFile(string filePath, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        public bool EncryptFile(string path, string fileName) {
            string filePath = Path.Combine(path, fileName);
            string fileText = FiletoString(filePath);
            byte[] encryptedFile = Encrypt(fileText);
            ByteArrayToFile(filePath, encryptedFile);
            return true;
        }
        public bool DecryptFile(string path, string filename) {
            string filePath = Path.Combine(path, filename);
            string fileText = FiletoString(filePath);
            byte[] encryptedFile = StringToByteArray(fileText);
            string decryptedString = Decrypt(encryptedFile);
            StringToFile(filePath, decryptedString);
            return true;
        }
        public byte[] StringToByteArray(string fileText) {
            byte[] encryptedFile = Encoding.Unicode.GetBytes(fileText);
            return encryptedFile;
        }
        public bool StringToFile(string filePath, string decryptedString) {
            bool resp = false;
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using var sr = new StringWriter((IFormatProvider)fs);
                    sr.WriteLine(decryptedString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
            return resp;
        }

        public byte[] Encrypt(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public string Decrypt(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
