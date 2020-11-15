using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PRIME_UCR.Application.Repositories.Multimedia;
using PRIME_UCR.Application.Services.Multimedia;
using System.Linq;
using PRIME_UCR.Application.Repositories.Appointments;
using PRIME_UCR.Application.Services.MedicalRecords;
using PRIME_UCR.Domain.Models.MedicalRecords;
using PRIME_UCR.Application.Repositories.MedicalRecords;
using PRIME_UCR.Infrastructure.Repositories.Sql.MedicalRecords;

namespace PRIME_UCR.Application.Implementations.MedicalRecords
{
    public class ChronicConditionService : IChronicConditionService
    {
        private readonly IChronicConditionRepository _repo;
        private readonly IChronicConditionListRepository _repoLista;
        public ChronicConditionService(IChronicConditionRepository repo, IChronicConditionListRepository repoLista)
        {
            _repo = repo;
            _repoLista = repoLista;
        }
        public async Task<IEnumerable<PadecimientosCronicos>> GetChronicConditionByRecordId(int recordId) 
        {
            return await _repo.GetByConditionAsync(i => i.IdExpediente == recordId);
        }

        public async Task<IEnumerable<ListaPadecimiento>> GetAll()
        {
            return await _repoLista.GetAllAsync();
        }

        public async Task<PadecimientosCronicos> InsertChronicConditionAsync(PadecimientosCronicos model)
        {
            await _repo.InsertAsync(model);
            return model;
        }
    }
}
