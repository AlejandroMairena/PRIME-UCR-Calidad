using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models;
using PRIME_UCR.Domain.Models.CheckLists;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.CheckLists
{
    class SqlInstanceChecklistRepository : GenericRepository<InstanceChecklist, int>, IInstanceChecklistRepository
    {
        public SqlInstanceChecklistRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
        //recuperar por la llave de la instancia de lista
        public async Task<IEnumerable<InstanceChecklist>> GetById(int id, string code)
        {
            return await this.GetByConditionAsync(InstanceChecklistModel => InstanceChecklistModel.PlantillaId == id
            && InstanceChecklistModel.IncidentCod == code);
        }
        //recuperar por codigo de incidente
        public async Task<IEnumerable<InstanceChecklist>> GetByIncidentCod(string cod)
        {
            return await this.GetByConditionAsync(InstanceChecklistModel => InstanceChecklistModel.IncidentCod == cod);
        }
        //recuperar por id de la plantilla
        public async Task<IEnumerable<InstanceChecklist>> GetByPlantillaId(int id)
        {
            return await this.GetByConditionAsync(InstanceChecklistModel => InstanceChecklistModel.PlantillaId == id);
        }
        
        public async Task DeleteAsync(int id, string code)
        {
            var existing = await _db.Set<InstanceChecklist>().FindAsync(code, id);
            if (existing != null)
            {
                _db.InstanceChecklist.Remove(existing);
            }
            await _db.SaveChangesAsync();
        }
    }
}
