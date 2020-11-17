using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.Dashboard
{
    public interface IDashboardRepository
    {
        Task<int> GetIncidentsCounterAsync(string modality);
    }
}
