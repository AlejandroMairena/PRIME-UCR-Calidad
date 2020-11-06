using PRIME_UCR.Domain.Models.CheckLists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Application.Repositories.CheckLists
{
    public interface IInstanceChecklistRepository : IGenericRepository<InstanceChecklist, int>
    {
        //recuperar por id
        Task<IEnumerable<InstanceChecklist>> GetByIdd(int id);
       
       //recuperar por id de la plantilla
        Task<IEnumerable<InstanceChecklist>> GetByPlantillaId(int id);
        //recuperar por codigo de incidente
        Task<IEnumerable<InstanceChecklist>> GetByIncidentCod(string cod);
       
        Task <InstanceChecklist> InsertInstanceChecklistAsync(InstanceChecklist list);
    }
}
