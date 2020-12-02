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
using PRIME_UCR.Domain.Constants;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Domain.Models.UserAdministration;
using PRIME_UCR.Infrastructure.DataProviders;
using RepoDb;
using RepoDb.Extensions;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.Incidents
{
    public class IncidentRepository : RepoDbRepository<Incidente, string>, IIncidentRepository
    {
        public IncidentRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public override async Task<Incidente> InsertAsync(Incidente model)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                var parameters = new Dictionary<string, object>
                {
                    {"CedulaAdmin", model.CedulaAdmin},
                    {"Modalidad", model.Modalidad},
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
        }

        public async Task<Incidente> GetIncidentByDateCodeAsync(int id) {
            return await _db.Incidents
                    .Include(p => p.Origen)
                    .Include(p => p.Destino)
                    .Include(p => p.EstadoIncidentes)
                    .FirstOrDefaultAsync(i => i.CodigoCita == id);
        }

        public async Task<Incidente> GetWithDetailsAsync(string code)
        {
            Incidente incident;
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                 incident =
                    (await connection.QueryAsync<Incidente>(code))
                    .FirstOrDefault();

                 if (incident != null)
                 {
                     incident.Cita =
                         (await connection.QueryAsync<Cita>(incident.CodigoCita))
                         .FirstOrDefault();


                     if (incident.Cita?.IdExpediente != null)
                         incident.Cita.Expediente =
                             (await connection.QueryAsync<Expediente>(incident.Cita.IdExpediente))
                             .FirstOrDefault();
                 }
            }

            var modelWithLocations = await _db.Incidents
                .AsNoTracking()
                .Include(i => i.Origen)
                .Include(i => i.Destino)
                .FirstOrDefaultAsync(i => i.Codigo == code);

            if (incident != null)
            {
                incident.Origen = modelWithLocations.Origen;
                incident.Destino = modelWithLocations.Destino;
            }

            return incident;
        }

        public async Task<IEnumerable<IncidentListModel>> GetIncidentListModelsAsync()
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                var sql =
                    // Incidente
                    @"
                        select *
                        from Incidente;
                    " +
                    // Cita
                    @"
                        select C.*
                        from Incidente I
                        join Cita C on C.Id = I.CodigoCita;
                    " +
                    // Destino - CentroUbicacion
                    @"
                        select CU.*
                        from Incidente
                        left join Ubicacion U on Incidente.IdDestino = U.Id
                        left join Centro_Ubicacion CU on U.Id = CU.Id
                    " +
                    // Estado
                    @"
                        select EI.*
                        from Incidente
                        join EstadoIncidente EI on Incidente.Codigo = EI.CodigoIncidente
                        where EI.Activo = 1;
                    ";

                using (var result = await connection.ExecuteQueryMultipleAsync(sql))
                {
                    var incidents = await result.ExtractAsync<Incidente>();
                    var appointments = await result.ExtractAsync<Cita>();
                    var destinations = await result.ExtractAsync<CentroUbicacion>();
                    var medicalCenters = await connection.QueryAllAsync<CentroMedico>();
                    var states = await result.ExtractAsync<EstadoIncidente>();
                    var origins =
                        _db.Incidents
                            .AsNoTracking()
                            .Include(i => i.Origen)
                            .Select(i => i.Origen);

                    return from i in incidents
                        join a in appointments
                            on i.CodigoCita equals a.Id
                        join s in states
                            on i.Codigo equals s.CodigoIncidente
                        join add in origins
                            on i.IdOrigen equals add?.Id
                            into orgs
                            from o in orgs.DefaultIfEmpty() // left join
                        join add in destinations
                            on i.IdDestino equals add?.Id
                            into cUs
                            from d in cUs.DefaultIfEmpty() // left join
                        join add in medicalCenters
                            on d?.CentroMedicoId equals add?.Id
                            into cMs
                            from mc in cMs.DefaultIfEmpty() // left join
                        select new IncidentListModel
                        {
                            Codigo = i.Codigo,
                            FechaHoraRegistro = a.FechaHoraCreacion,
                            Modalidad = i.Modalidad,
                            Origen = o?.DisplayName,
                            Estado = s.NombreEstado,
                            IdDestino = i.IdDestino,
                            Destino = mc?.Nombre
                        };
                }
            }
        }

        public async Task<Médico> GetAssignedOriginDoctor(string code)
        {
            var incident = await _db.Incidents
                .Include(i => i.Origen)
                .FirstOrDefaultAsync(i => i.Codigo == code);

            if (incident?.Origen is CentroUbicacion)
            {
                var cu = incident.Origen as CentroUbicacion;
                if (cu != null) return await GetDoctorById(cu.CedulaMedico);
            }

            return null;
        }

        public async Task<Médico> GetAssignedDestinationDoctor(string code)
        {
            var incident = await _db.Incidents
                .Include(i => i.Destino)
                .FirstOrDefaultAsync(i => i.Codigo == code);

            if (incident?.Destino is CentroUbicacion)
            {
                var cu = incident.Destino as CentroUbicacion;
                if (cu != null) return await GetDoctorById(cu.CedulaMedico);
            }

            return null;
        }

        public async Task<IEnumerable<OngoingIncident>> GetOngoingIncidentsAsync()
        {
            // query all incidents with requiered data
            var data =
                await _db.Incidents
                    .AsNoTracking()
                    .Include(i => i.Origen)
                    .Include(i => i.Destino)
                    .Include(i => i.UnidadDeTransporte)
                    .Include(i => i.EstadoIncidentes)
                    .ToListAsync();

            // filter out the incidents that are not ongoing
            return data
                .Where(i =>
                {
                    var currentState = i.EstadoIncidentes.First(s => s.Activo);
                    return IncidentStates.OngoingStates.Exists(s => s.Nombre == currentState.NombreEstado);
                })
                .Select(i => new OngoingIncident // create OngoingIncident DTO to return
                {
                    Origin = i.Origen,
                    Destination = i.Destino,
                    Incident = i,
                    TransportUnit = i.UnidadDeTransporte,
                    UnitType = new Modalidad { Tipo = i.Modalidad }
                });
        }

        public override async Task<Incidente> GetByKeyAsync(string key)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                var incident =
                    (await connection.QueryAsync<Incidente>(i => i.Codigo == key))
                    .FirstOrDefault();

                if (incident != null)
                    incident.Cita =
                        (await connection.QueryAsync<Cita>(c => c.Id == incident.CodigoCita))
                        .FirstOrDefault();

                return incident;
            }
        }

        private async Task<Médico> GetDoctorById(string id)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                return (await connection.ExecuteQueryAsync<Médico>(@"
                    select Persona.* from Persona
                    join Funcionario F on Persona.Cédula = F.Cédula
                    join Médico M on F.Cédula = M.Cédula
                    where M.Cédula = @Id
                ", new { Id = id }))
                .FirstOrDefault();
            }
        }

        public async Task<CambioIncidente> GetLastChange(string code)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                return (await connection.ExecuteQueryAsync<CambioIncidente>(@"
                    select CambioIncidente.* from CambioIncidente
                    where CodigoIncidente = @Id
                    order by FechaHora desc
                ", new { Id = code }))
                .FirstOrDefault();
            }
        }

        public async Task UpdateLastChange(CambioIncidente change)
        {
            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                var incident =
                    (await connection.QueryAsync<Incidente>(i => i.Codigo == change.CodigoIncidente))
                    .FirstOrDefault();
                if (incident != null)
                    change.Incidente = incident;
                await connection.InsertAsync(change);
            }
        }
    }
}
