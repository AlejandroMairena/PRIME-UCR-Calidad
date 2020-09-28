using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.Multimedia
{
    public interface IFileService
    {
        public Task<bool> StoreFile(string fileName, Stream fileStream);

    }
}
