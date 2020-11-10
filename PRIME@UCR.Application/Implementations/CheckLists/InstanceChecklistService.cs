using PRIME_UCR.Application.Services.CheckLists;
using PRIME_UCR.Application.Repositories;
using PRIME_UCR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Components;
using PRIME_UCR.Application.Repositories.CheckLists;
using PRIME_UCR.Domain.Models.CheckLists;

namespace PRIME_UCR.Application.Implementations.CheckLists
{
    /**
     * Class used to manage checklists and their items
     */
    public class InstanceChecklistService : IInstanceChecklistService
    {
        private readonly IInstanceChecklistRepository _instancechecklistRepository;
        private readonly IInstanceItemRepository _instanceItemRepository;
        // private readonly IItemRepository _itemRepository;

        public InstanceChecklistService(IInstanceChecklistRepository instancechecklistRepository, IInstanceItemRepository instanceItemRepository)//, IItemRepository itemRepository)
        {
            _instancechecklistRepository = instancechecklistRepository;
            _instanceItemRepository = instanceItemRepository;
            //_itemRepository = itemRepository;
        }

        public async Task<IEnumerable<InstanceChecklist>> GetAll()
        {
            IEnumerable<InstanceChecklist> lists = await _instancechecklistRepository.GetAllAsync();
            return lists;//.OrderBy(InstanceChecklist => InstanceChecklist.Orden);
        }
        public async Task<InstanceChecklist> InsertInstanceChecklist(InstanceChecklist list) 
        {
            return await _instancechecklistRepository.InsertAsync(list);
        }

        public async Task<InstanceChecklist> GetById(int id)
        {
            return await _instancechecklistRepository.GetByKeyAsync(id);
        }
        public async Task<IEnumerable<InstanceChecklist>> GetByIncidentCod(string cod)
        {
            IEnumerable<InstanceChecklist> lists = await _instancechecklistRepository.GetByIncidentCod(cod);
            return lists;
        }


        public async Task<InstanceChecklist> UpdateInstanceChecklist(InstanceChecklist list)
        {
            await _instancechecklistRepository.UpdateAsync(list);
            return list;
        }

        public async Task DeleteInstanceChecklist(int id, string cod)
        {
            await _instancechecklistRepository.DeleteAsync(id, cod);
        }

        public async Task<InstanciaItem> InsertInstanceItem(InstanciaItem instanceItem)
        {
            return await _instanceItemRepository.InsertAsync(instanceItem);
        }

        public async Task<IEnumerable<InstanciaItem>> GetItemsByIncidentCodAndCheckListId(string incidentCode, int checklistId) 
        {
            IEnumerable<InstanciaItem> items = await _instanceItemRepository.GetByIncidentCodAndCheckListId(incidentCode, checklistId);
            return items;
        }

        public async Task<IEnumerable<InstanciaItem>> GetCoreItems(string incidentCode, int checklistId) 
        {
            IEnumerable<InstanciaItem> items = await _instanceItemRepository.GetCoreItems(incidentCode, checklistId);
            return items;
        }

        public async Task<IEnumerable<InstanciaItem>> GetItemsByFatherId(string incidentCode, int checklistId, int itemId)
        {
            IEnumerable<InstanciaItem> items = await _instanceItemRepository.GetItemsByFatherId(incidentCode, checklistId, itemId);
            return items;
        }
    }
}
