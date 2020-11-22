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
using PRIME_UCR.Application.Services.UserAdministration;
using System.ComponentModel.DataAnnotations;
using PRIME_UCR.Application.Permissions.CheckLists;

namespace PRIME_UCR.Application.Implementations.CheckLists
{
    /**
     * Class used to manage checklists and their items
     */
    public partial class InstanceChecklistService : IInstanceChecklistService
    {
        private readonly IInstanceChecklistRepository _instancechecklistRepository;
        private readonly IInstanceItemRepository _instanceItemRepository;
        // private readonly IItemRepository _itemRepository;
        private readonly IPrimeSecurityService _primeSecurityService;

        public InstanceChecklistService(IInstanceChecklistRepository instancechecklistRepository,
                                        IInstanceItemRepository instanceItemRepository,
                                        IPrimeSecurityService primeSecurityService)//, IItemRepository itemRepository)
        {
            _instancechecklistRepository = instancechecklistRepository;
            _instanceItemRepository = instanceItemRepository;
            //_itemRepository = itemRepository;
            _primeSecurityService = primeSecurityService;
        }

        public async Task<IEnumerable<InstanceChecklist>> GetAll()
        {
            IEnumerable<InstanceChecklist> lists = await _instancechecklistRepository.GetAllAsync();
            return lists;//.OrderBy(InstanceChecklist => InstanceChecklist.Orden);
        }
        public async Task<InstanceChecklist> InsertInstanceChecklist(InstanceChecklist list)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
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

        public async Task<IEnumerable<InstanceChecklist>> GetByIdAndIncidentCode(int checkListId, string incidentCode)
        {
            return await _instancechecklistRepository.GetById(checkListId, incidentCode);
        }

        public async Task<InstanceChecklist> UpdateInstanceChecklist(InstanceChecklist list)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            await _instancechecklistRepository.UpdateAsync(list);
            return list;
        }

        public async Task<int> GetNumberOfItems(string incidentCode, int checkListId)
        {
            IEnumerable<InstanciaItem> items = await this.GetItemsByIncidentCodAndCheckListId(incidentCode, checkListId);
            int numberOfItems = items.Count();
            return numberOfItems;
        }

        public async Task<int> GetNumberOfCompletedItems(string incidentCode, int checkListId)
        {
            IEnumerable<InstanciaItem> items = await this.GetItemsByIncidentCodAndCheckListId(incidentCode, checkListId);
            int numberOfItems = 0;
            foreach(var tempItem in items)
            {
                if (tempItem.Completado == true)
                {
                    numberOfItems++;
                }
            }
            return numberOfItems;
        }

        public async Task DeleteInstanceChecklist(int id, string cod)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            await _instancechecklistRepository.DeleteAsync(id, cod);
        }

        public async Task<InstanciaItem> InsertInstanceItem(InstanciaItem instanceItem)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            return await _instanceItemRepository.InsertAsync(instanceItem);
        }

        public async Task<IEnumerable<InstanciaItem>> GetItemsByIncidentCodAndCheckListId(string incidentCode, int checklistId)
        {
            return await _instanceItemRepository.GetByIncidentCodAndCheckListId(incidentCode, checklistId);
        }

        public async Task<IEnumerable<InstanciaItem>> GetCoreItems(string incidentCode, int checklistId)
        {
            return await _instanceItemRepository.GetCoreItems(incidentCode, checklistId);
        }

        public async Task<IEnumerable<InstanciaItem>> GetItemsByFatherId(string incidentCode, int checklistId, int itemId)
        {
            return await _instanceItemRepository.GetItemsByFatherId(incidentCode, checklistId, itemId);
        }

        public async Task<InstanciaItem> UpdateItemInstance(InstanciaItem item)
        {
            await _primeSecurityService.CheckIfIsAuthorizedAsync(this.GetType());
            await _instanceItemRepository.UpdateAsync(item);
            return item;
        }
    }

    [MetadataType(typeof(InstanceChecklistServiceAuthorization))]
    public partial class InstanceChecklistService { }
}
