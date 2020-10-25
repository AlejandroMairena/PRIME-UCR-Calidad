using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Collections.Generic;
using System.Data;
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
                _db.Actions.Remove(existing);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<MultimediaContent>> GetByAppointmentAction(int citaId, string nombreAccion)
        {
            return await _db.Actions
                .Include(a => a.MultimediaContent)
                .Where(a => a.CitaId == citaId && a.NombreAccion == nombreAccion)
                .Select(a => a.MultimediaContent)
                .ToListAsync();
        }

        public async Task<Accion> InsertAsync(Accion action)
        {
            //_db.Actions.Add(action);
            //await _db.SaveChangesAsync();
            //return action;
            return await Task.Run(() =>
            {
                // raw sql
                using (var cmd = _db.DbConnection.CreateCommand())
                {
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }

                    cmd.CommandText =
                        $"EXECUTE dbo.InsertarAccion {action.CitaId}, '{action.NombreAccion}', {action.MultContId}";
                }

                return action;
            });
        }
    }
}
