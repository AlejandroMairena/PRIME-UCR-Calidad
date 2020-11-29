using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRIME_UCR.Application.Services.Dashboard
{
    public interface IFileManagerService
    {
         string createFile(List<Incidente> filteredIncidentsData);
    }
}
