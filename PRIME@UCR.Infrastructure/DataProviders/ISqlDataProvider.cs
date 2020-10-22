﻿using System.Data;
using Microsoft.EntityFrameworkCore;
using PRIME_UCR.Domain.Models;
using System.Threading.Tasks;
using PRIME_UCR.Domain.Models.Incidents;
using PRIME_UCR.Domain.Models.UserAdministration;

namespace PRIME_UCR.Infrastructure.DataProviders
{
    public interface ISqlDataProvider
    {
        IDbConnection DbConnection { get; set; }

        // dbsets
        DbSet<CheckList> CheckList { get; set; }
        DbSet<Pais> Countries { get; set; }
        DbSet<Provincia> Provinces { get; set; }
        DbSet<Canton> Cantons { get; set; }
        DbSet<Distrito> Districts { get; set; }
        DbSet<Ubicacion> Locations { get; set; }
        DbSet<Domicilio> HouseholdLocations { get; set; }
        DbSet<Internacional> InternationalLocations { get; set; }
        DbSet<CentroMedico> MedicalCenters { get; set; }
        DbSet<CentroUbicacion> MedicalCenterLocations { get; set; }
        DbSet<TrabajaEn> WorksOn { get; set; }
        DbSet<Modalidad> Modes { get; set; }
        DbSet<UnidadDeTransporte> TransportUnits { get; set; }
        DbSet<Estado> States { get; set; }
        DbSet<EstadoIncidente> IncidentStates { get; set; }
        DbSet<Incidente> Incidents { get; set; }

        DbSet<Usuario> Usuarios {get; set;}
        DbSet<Persona> People { get; set; }
        DbSet<Administrador> Adminstrators { get; set; }
        DbSet<AdministradorCentroDeControl> AdministratorsControlCenter { get; set; }
        DbSet<CoordinadorTécnicoMédico> MedicalTechnicians { get; set; }
        DbSet<EspecialistaTécnicoMédico> MedicalSpecialists { get; set; }
        DbSet<Funcionario> Functionaries { get; set; }
        DbSet<GerenteMédico> MedicalManagers { get; set; }
        DbSet<Médico> Doctors { get; set; }
        DbSet<NúmeroTeléfono> PhoneNumbers { get; set; }
        DbSet<Paciente> Patients { get; set; }
        DbSet<Perfil> Profiles { get; set; }
        DbSet<Permiso> Permissions { get; set; }
        DbSet<Pertenece> BelongsTo { get; set; }
        DbSet<TienePerfil> HasProfile { get; set; }
        DbSet<Permite> HasPermissionOf { get; set; }
        DbSet<Cita> Appointments { get; set; }
        DbSet<MultimediaContent> Multimedia_Contents { get; set; }
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();     
    }
}