using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.Incidents;
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
            throw new InvalidOperationException("Use overload with model and DateTime.");
        }
        public async Task<Incidente> InsertAsync(Incidente model, DateTime estimatedTime)
        {
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
                        $"EXECUTE dbo.InsertarNuevoIncidente NULL, NULL, {model.CedulaAdmin}, NULL, NULL, NULL, NULL, '{model.TipoModalidad}', '{new SqlDateTime(DateTime.Now).ToSqlString()}', '{new SqlDateTime(estimatedTime).ToSqlString()}'";
              
                    model.Codigo = cmd.ExecuteScalar() // returns a string
                        .ToString();
                }

                return model;
            });
        }

        public async Task<Incidente> GetWithDetailsAsync(string code)
        {
            return await _db.Incidents
                .Include(i => i.Cita)
                .Include(i => i.Origen)
                .Include(i => i.Destino)
                .FirstOrDefaultAsync(i => i.Codigo == code);
        }

        public new async Task<IEnumerable<Incidente>> GetAllAsync()
        {
            return await _db.Incidents
                .Include(i => i.Origen)
                .Include(i => i.Destino)
                .Include(i => i.EstadoIncidentes)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<IncidentListModel>> GetIncidentListModelsAsync()
        {
            return await Task.Run(() =>
            {

                var destinations =
                    from i in _db.Incidents
                    join u in _db.MedicalCenterLocations on i.IdDestino equals u.Id
                    join mc in _db.MedicalCenters on u.CentroMedicoId equals mc.Id
                    select new { MedicalCenter = mc, Id = i.IdDestino };

                var incidentStates = _db.IncidentStates
                    .Where(state => state.Activo);

                var incidentListModels = _db.Incidents
                    .Include(i => i.Origen)
                    .Include(i => i.Cita)
                    .Select(i =>
                    new IncidentListModel
                    {
                        Codigo = i.Codigo,
                        FechaHoraRegistro = i.Cita.FechaHoraCreacion,
                        Modalidad = i.TipoModalidad,
                        Origen = i.Origen,
                        IdDestino = i.IdDestino
                    });

                return
                    from i in incidentListModels
                    join add in destinations on i.IdDestino equals add.Id
                    into mCs
                    from mc in mCs.DefaultIfEmpty()
                    join state in incidentStates on i.Codigo equals state.CodigoIncidente
                    select new IncidentListModel
                    {
                        Codigo = i.Codigo,
                        FechaHoraRegistro = i.FechaHoraRegistro,
                        Modalidad = i.Modalidad,
                        Origen = i.Origen,
                        Estado = state.NombreEstado,
                        IdDestino = i.IdDestino,
                        Destino = mc != null ? mc.MedicalCenter : null
                    };
            }); 
        }

        public new async Task<Incidente> GetByKeyAsync(string key)
        {
            return await _db.Incidents
                .Include(i => i.Cita)
                .FirstOrDefaultAsync(i => i.Codigo == key);
        }
    }
}