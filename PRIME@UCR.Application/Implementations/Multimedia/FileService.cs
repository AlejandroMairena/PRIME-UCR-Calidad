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
        public async Task<bool> StoreFile(string fileName, Stream fileStream)
        {
            int buff_size = 16 * 1024;
            char[] buffer = new char[buff_size];
            byte[] bbuffer = new byte[buff_size];
            using (MemoryStream ms = new MemoryStream())
            {
                using (var reader = new StreamReader(fileStream))
                {
                    int read;
                    while ((read = await reader.ReadBlockAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        Console.WriteLine("Read: " + read);
                        for (int i = 0; i < read; i++)
                        {
                            char ch = buffer[i];
                            byte by = (byte)ch;
                            bbuffer[i] = by;
                            //bbuffer[i] = 0;
                        }
                        ms.Write(bbuffer, 0, read);
                    }
                }
                using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    ms.CopyTo(file);

                return true;
            }
        }
    }
}
