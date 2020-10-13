using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Infrastructure.DataProviders;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class IncidentRepository : GenericRepository<Incidente, string>, IIncidentRepository
    {
        public IncidentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public new async Task<Incidente> InsertAsync(Incidente model)
        {
            return await Task.Run(() =>
            {
                // raw sql
                using (var cmd = _db.DbConnection.CreateCommand())
                {
                    cmd.Connection.Open();
                    cmd.CommandText =
                        $"EXECUTE dbo.InsertarNuevoIncidente NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '{model.TipoModalidad}', '{model.FechaHoraRegistro.ToString("yyyy-MM-ddThh:mm:ss")}', '{model.FechaHoraEstimada.ToString("yyyy-MM-ddThh:mm:ss")}'";
              
                    model.Codigo = cmd.ExecuteScalar() // returns a string
                        .ToString();
                }

                return model;
            });
        }

        public async Task<Incidente> GetWithDetailsAsync(string code)
        {
            return await _db.Incidents
                .Include(i => i.Origen)
                .Include(i => i.Destino)
                .FirstOrDefaultAsync(i => i.Codigo == code);
        }
    }
}