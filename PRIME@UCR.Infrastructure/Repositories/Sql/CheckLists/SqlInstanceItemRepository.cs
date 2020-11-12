using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using PRIME_UCR.Domain.Models.CheckLists;
using PRIME_UCR.Infrastructure.DataProviders;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepoDb;

namespace PRIME_UCR.Infrastructure.Repositories.Sql.CheckLists
{
    class SqlInstanceItemRepository : GenericRepository<InstanciaItem, int>, IInstanceItemRepository
    {
        public SqlInstanceItemRepository(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }

        public async Task<IEnumerable<InstanciaItem>> GetByIncidentCodAndCheckListId(string incidentCode, int checklistId)
        {
            return await this.GetByConditionAsync(i => i.IncidentCod == incidentCode && i.PlantillaId == checklistId);
        }

        public async Task<IEnumerable<InstanciaItem>> GetCoreItems(string incidentCode, int checklistId)
        {
            return await this.GetByConditionAsync(itemModel => itemModel.IncidentCod == incidentCode && itemModel.PlantillaId == checklistId && itemModel.ItemPadreId == null);
        }

        public async Task<IEnumerable<InstanciaItem>> GetItemsByFatherId(string incidentCode, int checklistId, int itemId)
        {
            return await this.GetByConditionAsync(itemModel => itemModel.IncidentCodPadre == incidentCode && itemModel.PlantillaPadreId == checklistId && itemModel.ItemPadreId == itemId);
        }
    }
}
