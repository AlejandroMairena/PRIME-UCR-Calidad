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
       // private readonly IItemRepository _itemRepository;

        public InstanceChecklistService(IInstanceChecklistRepository instancechecklistRepository)//, IItemRepository itemRepository)
        {
            _instancechecklistRepository = instancechecklistRepository;
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
    }
}
