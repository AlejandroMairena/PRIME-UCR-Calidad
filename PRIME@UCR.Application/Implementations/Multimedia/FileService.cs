﻿using PRIME_UCR.Application.Services.Multimedia;
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
        public FileService()
        {
            FilePath = "PATH";
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

            return true;
        }
    }
}
