using System.Linq;
using System.Threading.Tasks;
using PRIME_UCR.Application.DTOs.MedicalRecords;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Application.Repositories.UserAdministration;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;

namespace PRIME_UCR.Application.Implementations.MedicalRecords
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repo;
        private readonly IPersonaRepository _personRepo;

        public MedicalRecordService(IMedicalRecordRepository repo)
        {
            _repo = repo;
        }

        public async Task<Expediente> GetByPatientIdAsync(string id)
        {
            return await _repo.GetByPatientIdAsync(id);
        }

        public async Task<Expediente> CreateMedicalRecordAsync(Expediente entity)
        {
            return await _repo.InsertAsync(entity);
        }

        public async Task<Expediente> GetByIdAsync(int id)
        {
            return await _repo.GetByKeyAsync(id);
        }

        public async Task<RecordViewModel> GetIncidentDetailsAsync(int id)
        {
            var record = await _repo.GetWithDetailsAsync(id);
            var person = await _personRepo.GetWithDetailsAsync(record.CedulaPaciente);
            var doctor = await _personRepo.GetWithDetailsAsync(record.CedulaMedicoDuenno);
            if (record != null)
            {
                var model = new RecordViewModel
                {
                    Cedula = person.Cédula,
                    Nombre = person.Nombre,
                    PrimerApellido = person.PrimerApellido,
                    SegundoApellido = person.SegundoApellido,
                    Sexo = person.Sexo,
                    FechaNacimineto = person.FechaNacimiento,
                    NombreMedico = doctor.Nombre,
                    PrimerApellidoMedico = doctor.PrimerApellido,
                    SegundoApellidoMedico = doctor.SegundoApellido,
                    IdExpediente = record.Id

    };

                return model;
            }

            return null;
        }
    }
}