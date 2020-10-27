using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Application.DTOs.Incidents;
using PRIME_UCR.Application.Repositories.Incidents;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Infrastructure.DataProviders;
using RepoDb;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class IncidentRepository : GenericRepository<Incidente, string>, IIncidentRepository
    {
        public IncidentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public override async Task<Incidente> InsertAsync(Incidente model)
        {
            await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);
            var parameters = new Dictionary<string, object>
            {
                {"CedulaAdmin", model.CedulaAdmin},
                {"Modalidad", model.TipoModalidad},
                {"FechaHoraRegistro", DateTime.Now},
                {"FechaHoraEstimada", model.Cita.FechaHoraEstimada},
                {"CedulaTecnicoCoordinador", null},
                {"CedulaTecnicoRevisor", null},
                {"IdOrigen", null},
                {"IdDestino", null},
                {"MatriculaTrans", null}
            };
            
            var result = await connection.ExecuteScalarAsync(
                "dbo.InsertarNuevoIncidente", parameters, CommandType.StoredProcedure);
            
            model.Codigo = result.ToString();

            return model;
        }
        public async Task<Incidente> GetWithDetailsAsync(string code)
        {
            await using var connection = new SqlConnection(_db.DbConnection.ConnectionString);

            var incident =
                connection.Query<Incidente>(code).FirstOrDefault();
            if (incident == null)
            {
                throw new ArgumentException("Invalid incident ID.");
            }
            
            incident.Cita =
                connection.Query<Cita>(incident.CodigoCita).FirstOrDefault();
            
            if (incident.Cita == null)
            {
                throw new ArgumentException("Invalid incident appointment.");
            }
            
            incident.Cita.Expediente =
                connection.Query<Expediente>(incident.Cita.IdExpediente).FirstOrDefault();
            
            var modelWithLocations = await _db.Incidents
                .Include(i => i.Origen)
                .Include(i => i.Destino)
                .FirstOrDefaultAsync(i => i.Codigo == code);

            incident.Origen = modelWithLocations.Origen;
            incident.Destino = modelWithLocations.Destino;

            return incident;
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
                        Origen = i.Origen.DisplayName,
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
                        Destino = mc != null ? mc.MedicalCenter.Nombre : null
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