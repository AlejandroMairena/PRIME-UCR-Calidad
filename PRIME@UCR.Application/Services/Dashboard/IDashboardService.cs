using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<int> GetIncidentCounterAsync(string modality);
    }
}
