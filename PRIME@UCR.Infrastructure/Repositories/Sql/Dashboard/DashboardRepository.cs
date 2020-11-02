using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.Dashboard;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Dashboard
{
    public class DashboardRepository : GenericRepository<Incidente, string>, IDashboardRepository
    {
        public DashboardRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        /**
         * Method used to get the list of all the incidents.
         * 
         * Return: List of incidents.
         */
        public async Task<List<Incidente>> GetAllIncidentsAsync()
        {
            return await _db.Incidents
                .Include(i => i.Cita)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
