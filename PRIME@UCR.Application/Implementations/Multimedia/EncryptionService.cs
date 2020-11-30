using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using PRIME_UCR.Application.Services.Multimedia;
using Newtonsoft.Json.Linq;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.SecretManager.V1;
using Google.Api.Gax.ResourceNames;
using Google.Api.Gax;

namespace PRIME_UCR.Application.Implementations.Multimedia
{
    public class EncryptionService : IEncryptionService
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public EncryptionService() {
            setKeyIV();
        }

        void setKeyIV()
        {
            // Some APIs, like Storage, accept a credential in their Create() method.
            // Explicitly use service account credentials by specifying the private key file.
            //GoogleCredential credential = GoogleCredential.FromFile("../PRIME@UCR/google_auth.json");
            // Create the client.
            SecretManagerServiceClientBuilder builder = new SecretManagerServiceClientBuilder
            {
                CredentialsPath = "../PRIME@UCR/google_auth.json"
            };
            SecretManagerServiceClient client = builder.Build();
            // Build the resource name.
            ProjectName projectName = new ProjectName("speedy-insight-297203");
            string nomb = "";
            // Call the API.
            SecretName keyName = new SecretName("speedy-insight-297203", "LlavePRIME");
            SecretName ivName = new SecretName("speedy-insight-297203", "IvPrime");

            Secret key = client.GetSecret(keyName);
            Secret iv = client.GetSecret(ivName);
            SecretVersionName keyV = new SecretVersionName("speedy-insight-297203", "LlavePRIME","1");
            SecretVersionName ivV = new SecretVersionName("speedy-insight-297203", "IvPrime", "2");
            AccessSecretVersionResponse resultKey = client.AccessSecretVersion(keyV);
            AccessSecretVersionResponse resultIv = client.AccessSecretVersion(ivV);
            string keyString = resultKey.Payload.Data.ToStringUtf8();
            string ivString = resultIv.Payload.Data.ToStringUtf8();
            //string jsonAppSettings = System.IO.File.ReadAllText("../PRIME@UCR/appsettings.json");
            //var jsonObjct = JObject.Parse(jsonAppSettings);
            //string keyString = (string)jsonObjct["Key"];
            //string ivString = (string)jsonObjct["IV"];
            byte[] ivByte = Convert.FromBase64String(ivString);
            byte[] keyByte = Convert.FromBase64String(keyString);
            SetKeyIV(ivByte, keyByte);
        }

        public void SetKeyIV(byte[] iv, byte[] key) {
            Key = key;
            IV = iv;
        }
        public byte[] generateKey() {
            byte[] generatedKey = null;
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                myRijndael.GenerateKey();
                generatedKey = myRijndael.Key;
            }
            return generatedKey;
        }
        public byte[] generateIV(){
            byte[] generatedIV = null;
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                myRijndael.GenerateIV();
                generatedIV = myRijndael.IV;
            }
            return generatedIV;
        }

        public string FiletoString(string path) {
            string fileString = "";
            if (File.Exists(path)) {
                //lee byte por byte y los guarda en 
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    byte[] chunk = new byte[fs.Length];
                    using (BinaryReader br = new BinaryReader(fs, new ASCIIEncoding()))
                    { 
                        chunk = br.ReadBytes((int)fs.Length);
                   }
                    fileString = Convert.ToBase64String(chunk);
                }
            }
            return fileString;
        }
        public byte[] FileToByteArray(string path) {
            byte[] dataArray = null;
            if (File.Exists(path))
            {
                //lee byte por byte y los guarda en 
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    byte[] chunk = new byte[fs.Length];
                    using (BinaryReader br = new BinaryReader(fs, new ASCIIEncoding()))
                    {
                        chunk = br.ReadBytes((int)fs.Length);
                    }
                    dataArray = chunk;
                }
            }
            return dataArray;
        }

        public bool ByteArrayToFile(string filePath, byte[] byteArray)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileStream.Write(byteArray, 0 , byteArray.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        public bool EncryptFile(string path) {
            string filePath = path;
            string fileText = FiletoString(filePath);
            byte[] encryptedFile = Encrypt(fileText);
            //string encryptedFileData = Encrypt(fileText);
            ByteArrayToFile(filePath, encryptedFile); //ESCRIBE EL ARCHIVO
            return true;
        }
        public bool DecryptFile(string path) {
            string filePath = path;
            //string fileText = FiletoString(filePath);
            byte[] encryptedFile = FileToByteArray(filePath);
            string decryptedString = Decrypt(encryptedFile);
            byte[] decryptedFile = Convert.FromBase64String(decryptedString);
            ByteArrayToFile(filePath, decryptedFile);

            return true;
        }
        public byte[] StringToByteArray(string fileText) {
            byte[] encryptedFile = System.Convert.FromBase64String(fileText);
            return encryptedFile;
        }
        public bool StringToFile(string filePath, string decryptedString) {
            bool resp = false;
            try
            {
                //using (StreamWriter outputFile = new StreamWriter(filePath))
                //{
                //    outputFile.WriteLine(decryptedString);
                //    return true;
                //}
                File.WriteAllText(filePath, decryptedString);

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
                            //VERIFICAR SI ToArray() funciona
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
