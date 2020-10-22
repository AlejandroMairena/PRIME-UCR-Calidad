using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Appointments
{
    public class ActionRepository : IActionRepository
    {
        protected readonly ISqlDataProvider _db;

        public ActionRepository(ISqlDataProvider dataProvider)
        {
            _db = dataProvider;
        }

        public async Task DeleteAsync(int citaId, string nombreAccion, int mcId)
        {
            var existing = await _db.Set<Accion>().FindAsync(citaId, nombreAccion, mcId);
            if (existing != null)
            {
                _db.Set<Accion>().Remove(existing);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Accion>> GetByAppointmentAction(int citaId, string nombreAccion)
        {
            return await _db.Set<Accion>()
                .Where(a => a.CitaId == citaId && a.NombreAccion == nombreAccion)
                .ToListAsync();
        }

        public async Task<Accion> InsertAsync(Accion action)
        {
            _db.Set<Accion>().Add(action);
            await _db.SaveChangesAsync();
            return action;
        }
    }
}
