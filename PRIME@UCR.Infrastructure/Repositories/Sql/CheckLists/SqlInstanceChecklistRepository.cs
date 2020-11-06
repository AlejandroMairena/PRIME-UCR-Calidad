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

        public async Task<InstanceChecklist> InsertInstanceChecklistAsync(InstanceChecklist list)
        {
            /*var parameters = new Dictionary<string, object>
             {
                 {"InstanciadoId", list.InstanciadoId},
                 { "PlantillaId", list.PlantillaId},
                 { "IncidentCod", list.IncidentCod},
                 {"Completado",0 },
                 { "FechaHoraInicio" ,null },
                 {"FechaHoraFinal", null }
             };*/
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
                        $"insert into dbo.InstanceChecklist  values ({list.InstanciadoId}, {list.PlantillaId} ,{list.IncidentCod},{0},{null},{null})";

                    list.InstanciadoId = int.Parse(s: cmd.ExecuteScalar().ToString());
                }
                return list;
            });
        }
        //recuperar por id de la instancia de lista
        public async Task<IEnumerable<InstanceChecklist>> GetByIdd(int id)
        {
            return await this.GetByConditionAsync(InstanceChecklistModel => InstanceChecklistModel.InstanciadoId == id);
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
    }
}
