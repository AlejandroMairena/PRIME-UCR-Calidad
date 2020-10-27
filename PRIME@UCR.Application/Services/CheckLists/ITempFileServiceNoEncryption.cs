using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.Multimedia
{
    // User Story PIG01IIC20-267 LG - Agregar imagen descriptiva a lista de chequeo
    // Temporal service to store files without encrypting them
    public interface ITempFileServiceNoEncryption
    {
        public string FilePath { get; set; }

        public Task<bool> StoreFile(string fileName, Stream fileStream);

    }
}
